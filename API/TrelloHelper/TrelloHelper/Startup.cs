using Common;
using Common.Configurations;
using Infrastructure.LUIS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TrelloHelper.Extensions;
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
                client.BaseAddress = new Uri("https://api.trello.com/1/");
            });

			var luisConfig = Configuration.GetSection(ConfigurationNames.LUISConfig).Get<LUISConfig>();
			services.AddHttpClient<ILUISClient, LUISClient>(client =>
			{
				client.BaseAddress = new Uri(luisConfig.APIUrl);
			});

			services.AddMemoryCache();
			services.RegisterServices(Configuration);
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
