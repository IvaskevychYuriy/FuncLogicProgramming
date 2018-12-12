
﻿using AutoMapper;
using BusinessLogic.Context;
using BusinessLogic.Intent;
using BusinessLogic.Query;
using Common;
using Common.Configurations;
using Common.Configurations.Options;
using Infrastructure.LUIS;
using Infrastructure.Trello;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            var trelloConfig = configuration.GetSection("Configuration");
            services.Configure<TrelloHelperConfiguration>(trelloConfig);

            services.AddHttpClient<TrelloClient>(client =>
            {
                client.BaseAddress = new Uri(trelloConfig.Get<TrelloHelperConfiguration>().TrelloAPIUrl);
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
			
			services.AddScoped<IIntentExecutor, IntentExecutor>();
			services.AddScoped<IContextProvider, ContextProvider>();
			services.AddScoped<IQueryProcessor, QueryProcessor>();
			services.AddScoped<IntentHandlerAggregateService>();

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
