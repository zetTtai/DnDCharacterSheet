using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Domain.Common;

namespace DnDCharacterSheet.Application.Common.Interfaces;
public interface IAuthService
{
    Task<Response?> ValidateEntityAccess<T>(BaseAuditableEntity? entity, string userId);
}
