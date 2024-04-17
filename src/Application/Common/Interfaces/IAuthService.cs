using DnDCharacterSheet.Domain.Common;

namespace DnDCharacterSheet.Application.Common.Interfaces;
public interface IAuthService
{
    Task<Result<T>?> ValidateEntityAccess<T>(BaseAuditableEntity? entity, string userId);
}
