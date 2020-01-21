using AzureFunctionDependencyInjection.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AzureFunctionDependencyInjection.Services
{
    public class MessageResponderService : IMessageResponderService
    {
        private MessageResponderConfiguration _messageResponderConfiguration;
        private ILogger<MessageResponderService> _logger;

        public MessageResponderService(IOptions<MessageResponderConfiguration> messageResponderConfiguration, ILogger<MessageResponderService> logger)
        {
            _messageResponderConfiguration = messageResponderConfiguration.Value;
            _logger = logger;
        }

        public string GetPositiveMessage()
        {
            _logger.LogDebug("Very Positive!");
            return _messageResponderConfiguration.PositiveResponseMessage;
        }

        public string GetNegativeMessage()
        {
            _logger.LogDebug("Very negative!");
            return _messageResponderConfiguration.NegativeResponseMessage;
        }
    }
}