using AzureFunctionDependencyInjection.Configurations;
using AzureFunctionDependencyInjection.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

[assembly: FunctionsStartup(typeof(AzureFunctionDependencyInjection.Startup))]

namespace AzureFunctionDependencyInjection
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<MessageResponderConfiguration>().Configure<IConfiguration>((messageResponderSettings, configuration) =>
            {
                configuration.GetSection("MessageResponder").Bind(messageResponderSettings);
            });

            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Services.AddSingleton<IMessageResponderService, MessageResponderService>();
            builder.Services.AddLogging(lb => lb.AddSerilog(logger));
        }
    }
}