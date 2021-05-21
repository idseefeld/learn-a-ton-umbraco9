using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Extensions;
using Microsoft.Extensions.Hosting;
using MyCustomUmbracoSolution.Models;
using Microsoft.OpenApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace MyCustomUmbracoSolution
{
	public class Startup
	{
		readonly string CorsAllowedOrigins = "_corsAllowedOrigins";

		private readonly IWebHostEnvironment _env;
		private readonly IConfiguration _config;

		/// <summary>
		/// Initializes a new instance of the <see cref="Startup"/> class.
		/// </summary>
		/// <param name="webHostEnvironment">The Web Host Environment</param>
		/// <param name="config">The Configuration</param>
		/// <remarks>
		/// Only a few services are possible to be injected here https://github.com/dotnet/aspnetcore/issues/9337
		/// </remarks>
		public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration config)
		{
			_env = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
			_config = config ?? throw new ArgumentNullException(nameof(config));
		}



		/// <summary>
		/// Configures the services
		/// </summary>
		/// <remarks>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		/// </remarks>
		public void ConfigureServices(IServiceCollection services)
		{
			var appConfig = new AppConfigModel();
			_config.Bind(appConfig);//makes settings from appsettings.json, user-secrets etc. available in this method
			services.Configure<AppConfigModel>(_config);//makes AppConfigModel injectable as Microsoft.Extensions.Options.IOptions<AppConfigModel>

#pragma warning disable IDE0022 // Use expression body for methods
			services.AddUmbraco(_env, _config)
				.AddBackOffice()
				.AddWebsite()
				.AddComposers()
				.Build();
#pragma warning restore IDE0022 // Use expression body for methods


			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend", Version = "v1" });
			});

			services.AddCors(o =>
				o.AddPolicy(name: CorsAllowedOrigins,
					builder =>
					{
						builder.WithOrigins(appConfig.AllowedHosts.ToArray());
					})
			);

		}

		/// <summary>
		/// Configures the application
		/// </summary>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

				// we may want swagger docs only be available in development environment. Otherwise move the following two lines out of the if clause.
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend v1"));
			}

			app.UseUmbraco()
				.WithMiddleware(u =>
				{
					u.WithBackOffice();
					u.WithWebsite();
				})
				.WithEndpoints(u =>
				{
					u.UseInstallerEndpoints();
					u.UseBackOfficeEndpoints();
					u.UseWebsiteEndpoints();
				});

#if DEBUG
			if (env.IsDevelopment())
			{
				app.UseEndpoints(endpoints =>
				{
					endpoints.MapGet("/debug-config", ctx =>
					{
						var config = (_config as IConfigurationRoot).GetDebugView();
						return ctx.Response.WriteAsync(config);
					});
				});
			}
#endif
		}
	}
}
