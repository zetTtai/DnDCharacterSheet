using System.Net;
using System.Reflection;
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Common.Security;

namespace DnDCharacterSheet.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse>(
    IUser user,
    IIdentityService identityService) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : IResponse, new()
{
    private readonly IUser _user = user;
    private readonly IIdentityService _identityService = identityService;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (_user.Id == null)
            {
                return new TResponse()
                {
                    Succeeded = false,
                    StatusCode = HttpStatusCode.Unauthorized,
                    Errors = []
                };
            }

            // Role-based authorization
            var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

            if (authorizeAttributesWithRoles.Any())
            {
                var authorized = false;

                foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                {
                    foreach (var role in roles)
                    {
                        var isInRole = await _identityService.IsInRoleAsync(_user.Id, role.Trim());
                        if (isInRole)
                        {
                            authorized = true;
                            break;
                        }
                    }
                }

                // Must be a member of at least one role in roles
                if (!authorized)
                {
                    return new TResponse()
                    {
                        Succeeded = false,
                        StatusCode = HttpStatusCode.Forbidden,
                        Errors = []
                    };
                }
            }

            // Policy-based authorization
            var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
            if (authorizeAttributesWithPolicies.Any())
            {
                foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                {
                    var authorized = await _identityService.AuthorizeAsync(_user.Id, policy);

                    if (!authorized)
                    {
                        return new TResponse()
                        {
                            Succeeded = false,
                            StatusCode = HttpStatusCode.Forbidden,
                            Errors = []
                        };
                    }
                }
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}
