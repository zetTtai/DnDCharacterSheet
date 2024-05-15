using System.Net;
using DnDCharacterSheet.Application.Common.Models;

namespace DnDCharacterSheet.Web.Infrastructure;
public static class ResponseExtensions
{
    public static IResult ToActionResult<T>(this Response<T> response) => MapStatusToResult(response.StatusCode, response.Errors, response.Value);

    public static IResult ToActionResult(this Response response) => MapStatusToResult<object>(response.StatusCode, response.Errors);

    private static IResult MapStatusToResult<T>(HttpStatusCode statusCode, IEnumerable<string> errors, T? value = default)
    {
        return statusCode switch
        {
            HttpStatusCode.OK => value is not null
                                    ? Results.Ok(value) 
                                    : Results.Ok(),
            HttpStatusCode.NoContent => Results.NoContent(),
            HttpStatusCode.Created => value is not null
                                    ? Results.Created("", value)
                                    : Results.Created(),

            HttpStatusCode.BadRequest => Results.BadRequest(errors),
            HttpStatusCode.NotFound => Results.NotFound(errors),
            HttpStatusCode.Forbidden => Results.Forbid(),
            HttpStatusCode.Unauthorized => Results.Unauthorized(),

            _ => Results.StatusCode((int)HttpStatusCode.InternalServerError),
        };
    }
}
