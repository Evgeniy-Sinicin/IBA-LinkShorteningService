using System.Net;
using IdentityServer4.Services;
using LinkShorteningService.IdentityServer.DataAccess.Data.Identity;
using LinkShorteningService.IdentityServer.DataAccess.Services;
using LinkShorteningService.IdentityServer.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LinkShorteningService.IdentityServer.Web
{
	public class Startup
	{
		public IWebHostEnvironment Environment { get; }
		public IConfiguration Configuration { get; }

		public Startup(IWebHostEnvironment environment, IConfiguration configuration)
		{
			Environment = environment;
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			string connectionString = Configuration.GetConnectionString("Default");

			services.AddControllersWithViews();

			services.AddDbContext<UsersContext>(options =>
				options.UseSqlServer(connectionString)
			);

			services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<UsersContext>()
				.AddDefaultTokenProviders();

			AddIdentityServer(services, connectionString);
		}

		private void AddIdentityServer(IServiceCollection services, string connectionString)
		{
			services.AddTransient<IProfileService, IdentityClaimsProfileService>();

			var builder = services.AddIdentityServer(options =>
			{
				options.Events.RaiseErrorEvents = true;
				options.Events.RaiseInformationEvents = true;
				options.Events.RaiseFailureEvents = true;
				options.Events.RaiseSuccessEvents = true;

				options.EmitStaticAudienceClaim = true;
			})
				.AddOperationalStore(options =>
				{
					options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString);
					options.EnableTokenCleanup = true;
					options.TokenCleanupInterval = Configuration.GetValue<int>("TokenCleanUpIntervalInSeconds");
				})
				.AddInMemoryIdentityResources(Config.IdentityResources)
				.AddInMemoryApiScopes(Config.ApiScopes)
				.AddInMemoryApiResources(Config.GetApiResources())
				.AddInMemoryClients(Config.GetClients(Configuration))
				.AddAspNetIdentity<AppUser>()
				.AddProfileService<IdentityClaimsProfileService>();

			builder.AddDeveloperSigningCredential();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseExceptionHandler(builder =>
			{
				builder.Run(async context =>
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

					var error = context.Features.Get<IExceptionHandlerFeature>();
					if (error != null)
					{
						context.Response.AddApplicationError(error.Error.Message);
						await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
					}
				});
			});

			var serilog = new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.Enrich.FromLogContext()
				.WriteTo.File(Configuration.GetValue<string>("LogFilePath"));

			loggerFactory.WithFilter(new FilterLoggerSettings
				{
					{ "IdentityServer4", LogLevel.Debug },
					{ "Microsoft", LogLevel.Warning },
					{ "System", LogLevel.Warning },
				}).AddSerilog(serilog.CreateLogger());

			app.UseStaticFiles();
			app.UseCors("AllowAll");

			app.UseRouting();
			app.UseIdentityServer();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});
		}
	}
}
