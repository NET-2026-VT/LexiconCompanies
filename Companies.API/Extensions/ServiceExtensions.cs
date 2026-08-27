using Companies.Infrastructure.Repositories;

namespace Companies.API.Extensions;

public static class ServiceExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICompanyRepsoitory, CompanyRepsoitory>();
        services.AddScoped<IPositionRepsoitory, PositionRepsoitory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped(provider => new Lazy<ICompanyRepsoitory>(() => provider.GetRequiredService<ICompanyRepsoitory>()));
        services.AddLazy<IPositionRepsoitory>();
    }

    public static void AddServiceLayer(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddLazy<ICompanyService>();
    }
}
