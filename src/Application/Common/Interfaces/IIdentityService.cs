using DnDCharacterSheet.Application.Common.Models;

namespace DnDCharacterSheet.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<Dictionary<string, string?>> GetUserNamesAsync(List<string> userIds);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Response Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Response> DeleteUserAsync(string userId);
}
