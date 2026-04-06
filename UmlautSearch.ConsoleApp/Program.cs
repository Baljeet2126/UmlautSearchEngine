using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using UmlautSearch.ConsoleApp.App;
using UmlautSearchEngine.Application.Builders;
using UmlautSearchEngine.Application.Interfaces;
using UmlautSearchEngine.Application.Services;
using UmlautSearchEngine.Domain.Configuration;
using UmlautSearchEngine.Domain.Interfaces;
using UmlautSearchEngine.Domain.Services;
using UmlautSearchEngine.Domain.Services.UmlautSearchEngine.Domain.Services;
using UmlautSearchEngine.Infrastructure;
using UmlautSearchEngine.Infrastructure.Database;
using UmlautSearchEngine.Infrastructure.Providers;
using UmlautSearchEngine.Infrastructure.Repositories;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // AppSettings 
        services.Configure<UmlautSettings>(
            context.Configuration.GetSection("UmlautSettings"));

        services.AddSingleton<VariationConfig>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<UmlautSettings>>().Value;

            return new VariationConfig
            {
                MaxVariations = settings.MaxVariations
            };
        });

        // Database setup 
        var connectionString = context.Configuration.GetConnectionString("Default");

        services.AddSingleton(new DatabaseInitializer(connectionString!));

        services.AddSingleton<IDataRepository>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var cs = config.GetConnectionString("Default");

            return new DataRepository(cs!);
        });

        // Infrastructure
        services.AddSingleton<IUmlautRuleProvider, UmlautRuleProvider>();

        // Domain
        services.AddSingleton<INameConverter, UmlautConverter>();
        services.AddSingleton<IVariationGenerator, VariationGenerator>();

        // Application
        services.AddSingleton<INameProcessingService, NameProcessingService>();
        services.AddSingleton<IQueryBuilder, QueryBuilder>();

        // App runner
        services.AddSingleton<AppRunner>();
    })
    .Build();

var initializer = host.Services.GetRequiredService<DatabaseInitializer>();
await initializer.InitializeAsync(); 
var app = host.Services.GetRequiredService<AppRunner>();



await app.RunAsync();