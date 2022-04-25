using AuthGuard.API.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AuthGuard.API.Tests
{
    public class StartupTest
    {
        [Fact]
        public void ConfigureServices_RegistersDependenciesCorrectly()
        {
            var hostingEnvironmentStub = new Mock<IHostingEnvironment>();

            hostingEnvironmentStub.Setup(x => x.ContentRootPath)
                .Returns("/Users/agitelgun/Desktop/Case/RoofStack/AuthGuard/AuthGuard.API");   
            
            hostingEnvironmentStub.Setup(x => x.EnvironmentName)
                .Returns("Development");

            IServiceCollection services = new ServiceCollection();
         
            var target = new Startup(hostingEnvironmentStub.Object);
            
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironmentStub.Object.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironmentStub.Object.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
            
            target.ConfigureServices(services);
            
            services.AddControllers();
            services.ConfigureSwaggerGenCollection();
            services.ConfigureDependencies();
            services.ConfigureAuthenticationCollection(configuration);
            services.AddRouting(options => options.LowercaseUrls = true);
            
            Assert.NotNull(target.Configuration);
        }
    }
}