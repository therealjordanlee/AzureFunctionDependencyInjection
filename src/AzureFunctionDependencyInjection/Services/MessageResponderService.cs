using AzureFunctionDependencyInjection.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AzureFunctionDependencyInjection.Services
{
    public class MessageResponderService : IMessageResponderService
    {
        private MessageResponderConfiguration _messageResponderConfiguration;
        private RandomResponderConfiguration _randomResponderConfiguration;
        private ILogger<MessageResponderService> _logger;

        public MessageResponderService(IOptions<MessageResponderConfiguration> messageResponderConfiguration,
            IOptions<RandomResponderConfiguration> randomResponderConfiguration,
            ILogger<MessageResponderService> logger)
        {
            _messageResponderConfiguration = messageResponderConfiguration.Value;
            _randomResponderConfiguration = randomResponderConfiguration.Value;
            _logger = logger;
        }

        public string GetPositiveMessage()
        {
            _logger.LogInformation("Very Positive!");
            return _messageResponderConfiguration.PositiveResponseMessage;
        }

        public string GetNegativeMessage()
        {
            _logger.LogInformation("Very negative!");
            return _messageResponderConfiguration.NegativeResponseMessage;
        }

        public string GetRandomMessage()
        {
            _logger.LogInformation("Random");
            return _randomResponderConfiguration.RandomMessage;
        }
    }
}