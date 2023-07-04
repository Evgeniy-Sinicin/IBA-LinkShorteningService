using LinkShorteningService.BusinessLogic.DTO;
using LinkShorteningService.BusinessLogic.Extensions;
using LinkShorteningService.BusinessLogic.Implementation;
using LinkShorteningService.BusinessLogic.Interfaces;
using LinkShorteningService.Properties;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace LinkShorteningService
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
            services.AddAuthentication("Bearer")
                .AddJwtBearer(o =>
                {
                    o.Authority = Configuration.GetValue<string>("IdentityServerUrl");
                    o.Audience = "links";
                    o.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Api", policy => policy.RequireClaim("scope", "LSS_api.CRUD"));
                options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "user"));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(Configuration.GetValue<string>("AngularClientUrl"))
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            
            services.AddControllers();

            string connStr = Configuration.GetConnectionString(Resources.connectionString);
            services.RegisterEfAsIUnitOfWork(connStr);

            services.AddTransient<ILinkService, LinkService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("default");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
