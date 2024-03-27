using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Domain.Constants;
using DnDCharacterSheet.Infrastructure.Data;
using DnDCharacterSheet.Infrastructure.Data.Interceptors;
using DnDCharacterSheet.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connStr = Environment.GetEnvironmentVariable("APPSETTING_CONNECTION_STRING");
        if (string.IsNullOrEmpty(connStr))
        {
            Console.WriteLine("CONNECTION_STRING environment variable is not set.");
        }

        var connStr2 = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        if (string.IsNullOrEmpty(connStr2))
        {
            Console.WriteLine("CONNECTION_STRING environment variable is not set.");
        }

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            Console.WriteLine("'DefaultConnection' not found, looking for environment variable ");
            connectionString = Environment.GetEnvironmentVariable("APPSETTING_CONNECTION_STRING")
                            ?? Environment.GetEnvironmentVariable("CONNECTION_STRING");
        }

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IIdentityService, IdentityService>();

        services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

        return services;
    }
}
