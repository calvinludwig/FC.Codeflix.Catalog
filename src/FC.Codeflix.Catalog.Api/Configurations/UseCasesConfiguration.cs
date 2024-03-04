using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.Infra.Data.EF;
using FC.Codeflix.Catalog.Infra.Data.EF.Repositories;

namespace FC.Codeflix.Catalog.Api.Configurations;

public static class UseCasesConfiguration
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddRepositories();
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateCategory).Assembly)
        );
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        return services;
    }
}
