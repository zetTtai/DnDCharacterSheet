using System.Net;

namespace DnDCharacterSheet.Web.Infrastructure;
public static class ResultExtensions
{
    public static IResult ToActionResult<T>(this Result<T> result) => MapStatusToResult(result.StatusCode, result.Errors, result.Value);

    public static IResult ToActionResult(this Result result) => MapStatusToResult<object>(result.StatusCode, result.Errors);

    private static IResult MapStatusToResult<T>(HttpStatusCode statusCode,IEnumerable<string> errors, T? value = default)
    {
        return statusCode switch
        {
            HttpStatusCode.OK => value is not null
                                    ? Results.Ok(value) 
                                    : Results.Ok(),
            HttpStatusCode.NoContent => Results.NoContent(),

            HttpStatusCode.NotFound => Results.NotFound(errors),
            HttpStatusCode.Forbidden => Results.Forbid(),
            HttpStatusCode.BadRequest => Results.BadRequest(errors),

            _ => Results.StatusCode((int)HttpStatusCode.InternalServerError),
        };
    }
}
