using System.Net;
using Microsoft.AspNetCore.Identity;

namespace DnDCharacterSheet.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(HttpStatusCode.BadRequest, result.Errors.Select(e => e.Description));
    }
}
