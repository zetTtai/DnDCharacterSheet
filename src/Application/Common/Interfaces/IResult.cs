using System.Net;

namespace DnDCharacterSheet.Application.Common.Interfaces;

public interface IResult
{
    bool Succeeded { get; set; }
    string[] Errors { get; set; }
    HttpStatusCode StatusCode { get; set; }
}

public interface IResult<T> : IResult
{
    T? Value { get; set; }
}
