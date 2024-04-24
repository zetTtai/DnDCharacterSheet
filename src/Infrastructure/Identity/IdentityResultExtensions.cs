using System.Net;
using DnDCharacterSheet.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace DnDCharacterSheet.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static Response ToResponse(this IdentityResult result)
    {
        return result.Succeeded
            ? Response.Success()
            : Response.Failure(HttpStatusCode.BadRequest, result.Errors.Select(e => e.Description));
    }
}
