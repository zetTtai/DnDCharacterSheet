using System.Net;
using DnDCharacterSheet.Application.Common.Interfaces;

namespace DnDCharacterSheet.Application.Common.Models;

public class Response : IResponse
{
    public bool Succeeded { get; set; }

    public string[] Errors { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public Response()
    {
        Succeeded = true;
        Errors = [];
    }

    internal Response(bool succeeded, HttpStatusCode httpStatusCode, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
        StatusCode = httpStatusCode;
    }


    public static Response Success(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    {
        return new Response(true, httpStatusCode, Array.Empty<string>());
    }

    public static Response Failure(HttpStatusCode httpStatusCode, IEnumerable<string> errors)
    {
        return new Response(false, httpStatusCode, errors);
    }
}

public class Response<T> : Response, IResponse<T>
{
    public T? Value { get; set; }

    public Response() : base() { }

    public Response(bool succeeded, HttpStatusCode httpStatusCode, IEnumerable<string> errors, T? value = default)
        : base(succeeded, httpStatusCode, errors)
    {
        Value = value;
    }

    public static Response<T> Success(T value, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    {
        return value is null
            ? throw new ArgumentNullException(nameof(value), "Value cannot be null.")
            : new Response<T>(true, httpStatusCode, Array.Empty<string>(), value);
    }

    public static new Response<T> Failure(HttpStatusCode httpStatusCode, IEnumerable<string> errors)
    {
        return new Response<T>(false, httpStatusCode, errors);
    }
}
