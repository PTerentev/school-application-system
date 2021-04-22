using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApplicationSystem.Web.Infrastructure.ServiceExtensions;
using ApplicationSystem.Web.Infrastructure;
using ApplicationSystem.UseCases.Extensions;
using ApplicationSystem.Infrastructure.Extensions;

namespace ApplicationSystem.Web
{
    /// <summary>
    /// Start up class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddUseCases();
            services.AddControllers();
            services.AddSwaggerServices();
            services.AddDomainServices(Configuration);
            services.AddAuthenticationServices(Configuration);
            services.AddConfigurationOptions(Configuration);
            services.AddCorsPolicies(Configuration);
            services.AddInfrastructure();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Web host environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApplicationSystem.Web v1"));
            }

            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseCors(CorsPolicyNames.DevCorsPolicyName);
            }
            else
            {
                app.UseCors(CorsPolicyNames.ProductionPolicyName);
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}
