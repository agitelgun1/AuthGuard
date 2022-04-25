using Microsoft.Extensions.Hosting;
using Xunit;

namespace AuthGuard.API.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void CreateHostBuilder_Should_Work_As_Expected()
        {
            var args = new[] {"CreateHostBuilder"};
            var createHostBuilder = Program.CreateHostBuilder(args);

            Assert.IsAssignableFrom<IHostBuilder>(createHostBuilder);
        }
    }
}