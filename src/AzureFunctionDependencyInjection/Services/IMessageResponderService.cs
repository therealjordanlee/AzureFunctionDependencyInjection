namespace AzureFunctionDependencyInjection.Services
{
    public interface IMessageResponderService
    {
        string GetPositiveMessage();

        string GetNegativeMessage();
    }
}