namespace DnDCharacterSheet.Web.Infrastructure;

public abstract class EndpointGroupBase
{
    public abstract void Map(WebApplication app);


    /// <summary>
    /// Generates an array of HTTP status codes for API responses based on provided conditions.
    /// </summary>
    /// <param name="success">The success status code to include in every response array.</param>
    /// <param name="not_found">If true, includes the 404 Not Found status code.</param>
    /// <param name="bad_request">If true, includes the 400 Bad Request status code.</param>
    /// <param name="forbidden">If true, includes the 403 Forbidden status code.</param>
    /// <param name="unauthorized">If true, includes the 401 Unauthorized status code. Defaults to true.</param>
    /// <returns>An array of integers representing the HTTP status codes for the API response.</returns>
    public static int[] GetStatusCodes(int success, bool not_found = false, bool bad_request = false, bool forbidden = false, bool unauthorized = true)
    {
        List<int> statusCodes = [success];
        if (not_found) statusCodes.Add(StatusCodes.Status404NotFound);
        if (bad_request) statusCodes.Add(StatusCodes.Status400BadRequest);
        if (forbidden) statusCodes.Add(StatusCodes.Status403Forbidden);
        if (unauthorized) statusCodes.Add(StatusCodes.Status401Unauthorized);

        return [.. statusCodes];
    }
}
