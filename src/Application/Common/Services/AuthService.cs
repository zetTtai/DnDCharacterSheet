using System.Net;
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Domain.Common;
using DnDCharacterSheet.Domain.Constants;

namespace DnDCharacterSheet.Application.Common.Services;
internal class AuthService(IIdentityService identityService) : IAuthService
{
    private readonly IIdentityService _identityService = identityService;
    public async Task<bool> IsOwner(BaseAuditableEntity entity, string userId)
    {
        bool isAuthorized = entity.CreatedBy == userId || await _identityService.IsInRoleAsync(userId, Roles.Administrator);

        return isAuthorized;
    }
}
