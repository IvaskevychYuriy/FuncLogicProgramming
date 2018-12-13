using AutoMapper;
using BusinessLogic.Context;
using BusinessLogic.Intent;
using BusinessLogic.Query;
using Common;
using Common.Configurations;
using Common.Configurations.Options;
using Infrastructure.LUIS;
using Infrastructure.Trello;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using TrelloHelper.BusinessLogic.Context;
using TrelloHelper.BusinessLogic.Intent;
using TrelloHelper.BusinessLogic.Intent.Models;
using TrelloHelper.BusinessLogic.Query;
using TrelloHelper.Infrastructure.LUIS;
using TrelloHelper.Infrastructure.Trello;

namespace TrelloHelper.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection RegisterAutomapper(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(Startup), typeof(QueryProcessor));

			return services;
		}

		public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<ContextEntryOptions>(GetConfig(configuration, ConfigurationNames.ContextEntryOptions));

			return services;
		}

		public static IServiceCollection RegisterHttpClients(this IServiceCollection services, IConfiguration configuration)
		{
            var trelloConfig = GetConfig(configuration, ConfigurationNames.TrelloConfig);
            services.Configure<TrelloConfig>(trelloConfig);

            services.AddHttpClient<ITrelloClient, TrelloClient>(client =>
            {
                client.BaseAddress = new Uri(trelloConfig.Get<TrelloConfig>().APIUrl);
            });

			var luisConfig = GetConfig(configuration, ConfigurationNames.LUISConfig).Get<LUISConfig>();
			services.AddHttpClient<ILUISClient, LUISClient>(client =>
			{
				client.BaseAddress = new Uri(luisConfig.APIUrl);
			});

			return services;
		}

		public static IServiceCollection RegisterMemoryCache(this IServiceCollection services, IConfiguration configuration)
		{
			var memoryCacheConfig = GetConfig(configuration, ConfigurationNames.MemoryCacheConfig).Get<MemoryCacheConfig>();
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
			
			services.AddTransient<IIntentExecutor, IntentExecutor>();
			services.AddTransient<IContextProvider, ContextProvider>();
			services.AddTransient<IQueryProcessor, QueryProcessor>();
			services.AddTransient<IntentHandlerAggregateService>();
			services.AddTransient<ITrelloUserInfoAccessor, TrelloUserInfoAccessor>();
			services.AddHttpContextAccessor();
			services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

			return services;
		}
		
		public static IServiceCollection RegisterIntentHandlers(this IServiceCollection services)
		{
			services.Scan(scan => scan
				.FromAssemblyOf<IntentBase>()
				.AddClasses(cfg => cfg.AssignableTo<IIntentHandler>())
				.AsImplementedInterfaces()
				.WithScopedLifetime());

			return services;
		}

		private static IConfigurationSection GetConfig(IConfiguration configuration, string subSection)
		{
			return configuration.GetSection($"{ConfigurationNames.Root}:{subSection}");
		}
	}
}
