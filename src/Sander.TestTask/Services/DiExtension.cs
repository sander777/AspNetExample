using Sander.TestTask.Domain.Repositories;
using Sander.TestTask.Persistance.Mssql;
using Sander.TestTask.Persistance.Mssql.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Options;

namespace Sander.TestTask.Services;

public static class DiExtension
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppConfiguration>(configuration);
        services.Configure<SqlServerSettings>(configuration.GetSection("SqlServerSettings"));
        return services;
    }

    public static IServiceCollection AddSqlServer(this IServiceCollection services)
    {
        var dbSettings = services.BuildServiceProvider().GetRequiredService<IOptions<SqlServerSettings>>().Value;
        services.AddDbContext<MarketDbContext>(options => options.UseSqlServer(dbSettings.ConnectionString));
        var dbContext = services.BuildServiceProvider().GetRequiredService<MarketDbContext>();
        dbContext.Database.Migrate();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IMarketItemsRepository, MarketItemRepository>();
        services.AddTransient<IAuctionRepository, AuctionRepository>();
        return services;
    }

    public static ConfigurationManager AddConfigurationSources(this ConfigurationManager configuration)
    {
        var source = new JsonConfigurationSource
        {
            Path = "SanderTestTaskAppSettings.json"
        };
        configuration.Sources.Add(source);

        return configuration;
    }
}
