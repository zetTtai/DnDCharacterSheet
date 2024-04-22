using System.Net;

namespace DnDCharacterSheet.Application.Common.Interfaces;

public interface IResponse
{
    bool Succeeded { get; set; }
    string[] Errors { get; set; }
    HttpStatusCode StatusCode { get; set; }
}

public interface IResponse<T> : IResponse
{
    T? Value { get; set; }
}
