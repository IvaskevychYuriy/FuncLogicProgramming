using Common;
using Common.Configurations;
using Common.Configurations.Options;
using Infrastructure.LUIS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TrelloHelper.Infrastructure.LUIS;
using TrelloHelper.Infrastructure.Trello;

namespace TrelloHelper
{
	public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<TrelloHelperConfiguration>(Configuration.GetSection("Configuration"));

			// TODO: move this somewhere
            services.AddHttpClient<TrelloHttpClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.github.com/");
            });

			var luisConfig = Configuration.GetSection(ConfigurationNames.LUISConfig).Get<LUISConfig>();
			services.AddHttpClient<ILUISClient, LUISClient>(client =>
			{
				client.BaseAddress = new Uri(luisConfig.APIUrl);
			});

			var memoryCacheConfig = Configuration.GetSection(ConfigurationNames.MemoryCacheConfig).Get<MemoryCacheConfig>();
			services.AddMemoryCache(config =>
			{
				config.ExpirationScanFrequency = TimeSpan.FromMinutes(memoryCacheConfig.ExpirationScanMinutes);
				config.SizeLimit = memoryCacheConfig.SizeLimit;
			});

			services.Configure<ContextEntryOptions>(Configuration.GetSection(ConfigurationNames.ContextEntryOptions));
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
			
            app.UseMvc();
        }
    }
}