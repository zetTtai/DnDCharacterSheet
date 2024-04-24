using System.Net;
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Domain.Common;
using DnDCharacterSheet.Domain.Constants;

namespace DnDCharacterSheet.Application.Common.Services;
internal class AuthService(IIdentityService identityService) : IAuthService
{
    private readonly IIdentityService _identityService = identityService;
    public async Task<Response?> ValidateEntityAccess<T>(BaseAuditableEntity? entity, string userId)
    {
        if (entity == null)
        {
            return Response.Failure(HttpStatusCode.NotFound, ["Entity not found."]);
        }

        bool isAuthorized = entity.CreatedBy == userId || await _identityService.IsInRoleAsync(userId, Roles.Administrator);

        return !isAuthorized 
            ? Response.Failure(HttpStatusCode.Forbidden, ["User does not have access to this entity."]) 
            : null;
    }
}
