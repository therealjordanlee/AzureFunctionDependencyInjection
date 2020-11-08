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
            // Registering Configurations (IOptions pattern)
            AddConfiguration<MessageResponderConfiguration>(builder, "MessageResponder");
            AddConfiguration<RandomResponderConfiguration>(builder, "RandomResponder");
            //builder
            //    .Services
            //    .AddOptions<MessageResponderConfiguration>()
            //    .Configure<IConfiguration>((messageResponderSettings, configuration) =>
            //    {
            //        configuration
            //        .GetSection("MessageResponder")
            //        .Bind(messageResponderSettings);
            //    });

            // Registering Serilog provider
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            builder.Services.AddLogging(lb => lb.AddSerilog(logger));

            // Registering services
            builder
                .Services
                .AddSingleton<IMessageResponderService, MessageResponderService>();
        }

        private void AddConfiguration<TOptions>(IFunctionsHostBuilder builder, string section) where TOptions : class
        {
            builder.Services
                .AddOptions<TOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(section).Bind(settings);
                });
        }
    }
}