using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AuthGuard.API.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;

namespace AuthGuard.API.Tests.Middleware
{
    public class ErrorMiddlewareTests
    {
        [Fact]
        public async Task
            InvokeRequest_Should_WorkedProperly()
        {
            var headers = new Dictionary<string, StringValues>()
            {
                {"X-Forwarded-For", "123456"}
            };

            var httpContextMoq = new Mock<HttpContext>();
            httpContextMoq.Setup(x => x.Request.Headers)
                .Returns(new HeaderDictionary(headers));
            httpContextMoq.Setup(x => x.Items)
                .Returns(new Dictionary<object, object>());

            var httpContext = httpContextMoq.Object;

            var requestDelegate = new RequestDelegate(
                (innerContext) => Task.FromResult(0));

            var mockLogger = new Mock<ILogger<ErrorMiddleware>>();

            var middleware = new ErrorMiddleware(requestDelegate, mockLogger.Object);

            await middleware.Invoke(httpContext);

            Assert.True(httpContext.Request.Headers.ContainsKey("X-Forwarded-For"));
        }

        [Fact]
        public async Task
            InvokeRequest_Should_CatchException()
        {
            var mockLogger = new Mock<ILogger<ErrorMiddleware>>();

            var errorMiddleware = new ErrorMiddleware((innerHttpContext) => { throw new Exception("Test exception"); },
                mockLogger.Object);

            var context = new DefaultHttpContext();

            await errorMiddleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var text = await reader.ReadToEndAsync();

            Assert.True(string.IsNullOrEmpty(text));
        }

        [Fact]
        public async Task
            InvokeRequest_Should_CatchExceptionWithIpAddress()
        {
            var headers = new KeyValuePair<string, StringValues>("X-Forwarded-For", "123456");
            var mockLogger = new Mock<ILogger<ErrorMiddleware>>();
            var errorMiddleware = new ErrorMiddleware((innerHttpContext) => { throw new Exception("Test exception"); },
                mockLogger.Object);

            var context = new DefaultHttpContext();

            context.Request.Headers.Add(headers);

            await errorMiddleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var text = await reader.ReadToEndAsync();

            Assert.True(string.IsNullOrEmpty(text));
        }
    }
}