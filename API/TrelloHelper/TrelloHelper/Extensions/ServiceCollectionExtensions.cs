using BusinessLogic.Intent;
using Common;
using Common.Configurations;
using Common.Configurations.Options;
using Infrastructure.LUIS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TrelloHelper.BusinessLogic.Intent;
using TrelloHelper.BusinessLogic.Intent.Handlers;
using TrelloHelper.Infrastructure.LUIS;
using TrelloHelper.Infrastructure.Trello;

namespace TrelloHelper.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
		{
			return services
				.RegisterOptions(configuration)
				.RegisterHttpClients(configuration)
				.RegisterMemoryCache(configuration)
				.RegisterDependencies(configuration);
		}

		public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<ContextEntryOptions>(configuration.GetSection(ConfigurationNames.ContextEntryOptions));

			return services;
		}

		public static IServiceCollection RegisterHttpClients(this IServiceCollection services, IConfiguration configuration)
		{
			// Trello
			// TODO: rework
			services.Configure<TrelloHelperConfiguration>(configuration.GetSection("Configuration"));

			// TODO: move this somewhere
			services.AddHttpClient<TrelloHttpClient>(client =>
			{
				client.BaseAddress = new Uri("https://api.github.com/");
			});

			// LUIS
			var luisConfig = configuration.GetSection(ConfigurationNames.LUISConfig).Get<LUISConfig>();
			services.AddHttpClient<ILUISClient, LUISClient>(client =>
			{
				client.BaseAddress = new Uri(luisConfig.APIUrl);
			});

			return services;
		}

		public static IServiceCollection RegisterMemoryCache(this IServiceCollection services, IConfiguration configuration)
		{
			var memoryCacheConfig = configuration.GetSection(ConfigurationNames.MemoryCacheConfig).Get<MemoryCacheConfig>();
			services.AddMemoryCache(config =>
			{
				config.ExpirationScanFrequency = TimeSpan.FromMinutes(memoryCacheConfig.ExpirationScanMinutes);
				config.SizeLimit = memoryCacheConfig.SizeLimit;
			});

			return services;
		}
		
		public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
		{
			services.RegisterIntentHandlers();

			// Here simple dependencies are registered, e.g. <IService, ConcreteService>

			return services;
		}
		
		public static IServiceCollection RegisterIntentHandlers(this IServiceCollection services)
		{
			services.AddScoped<IIntentHandlersContext, IntentHandlersContext>();
			services.Scan(scan => scan
				.FromAssemblyOf<TrelloIntentHandlerBase>()
				.AddClasses(cfg => cfg.AssignableTo<IIntentHandler>())
				.AsImplementedInterfaces()
				.WithScopedLifetime());

			return services;
		}
	}
}
