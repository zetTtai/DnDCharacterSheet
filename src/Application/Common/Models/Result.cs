using System.Net;
using DnDCharacterSheet.Application.Common.Interfaces;

public class Result : IResult
{
    public Result()
    {
        Succeeded = true;
        Errors = [];
    }

    internal Result(bool succeeded, HttpStatusCode httpStatusCode, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
        StatusCode = httpStatusCode;
    }

    public bool Succeeded { get; set; }

    public string[] Errors { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public static Result Success(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    {
        return new Result(true, httpStatusCode, Array.Empty<string>());
    }

    public static Result Failure(HttpStatusCode httpStatusCode, IEnumerable<string> errors)
    {
        return new Result(false, httpStatusCode, errors);
    }
}

public class Result<T> : Result, IResult<T>
{
    public T? Value { get; set; }

    public Result() : base() { }

    public Result(bool succeeded, HttpStatusCode httpStatusCode, IEnumerable<string> errors, T? value = default)
        : base(succeeded, httpStatusCode, errors)
    {
        Value = value;
    }

    public static Result<T> Success(T value, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    {
        return value is null
            ? throw new ArgumentNullException(nameof(value), "Value cannot be null.")
            : new Result<T>(true, httpStatusCode, Array.Empty<string>(), value);
    }

    public static new Result<T> Failure(HttpStatusCode httpStatusCode, IEnumerable<string> errors)
    {
        return new Result<T>(false, httpStatusCode, errors, default);
    }
}
