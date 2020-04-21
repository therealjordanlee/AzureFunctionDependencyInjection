using AzureFunctionDependencyInjection.Configurations;
using AzureFunctionDependencyInjection.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

[assembly: FunctionsStartup(typeof(AzureFunctionDependencyInjection.Startup))]

namespace AzureFunctionDependencyInjection
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Registering Configurations (IOptions pattern)
            builder
                .Services
                .AddOptions<MessageResponderConfiguration>()
                .Configure<IConfiguration>((messageResponderSettings, configuration) =>
                {
                    configuration
                    .GetSection("MessageResponder")
                    .Bind(messageResponderSettings);
                });

            // Registering Serilog provider
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            builder.Services.AddLogging(lb => lb.ClearProviders().AddSerilog(logger));

            // Registering services
            builder
                .Services
                .AddSingleton<IMessageResponderService, MessageResponderService>();
        }
    }
}