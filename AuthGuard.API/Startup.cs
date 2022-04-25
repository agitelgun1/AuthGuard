using AuthGuard.API.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthGuard.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
            services.AddRouting(options => options.LowercaseUrls = true);
            services.ConfigureSwaggerGenCollection();
            services.ConfigDependencies(Configuration);
            services.ConfigureAuthenticationCollection(Configuration);
            services.ConfigureDependencies();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUIBuilder();
            app.UseAuthentication();
            app.UseMiddleware();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}