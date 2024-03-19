using System.Reflection;
using CleanArchitecture.Application.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());

        _ = services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        _ = services.AddMediatR(cfg =>
        {
            _ = cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            _ = cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            _ = cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            _ = cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            _ = cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });

        return services;
    }
}
