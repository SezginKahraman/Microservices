namespace Microservices.UI.Services.Interfaces
{
    public interface IClientCredentialTokenService
    {
        Task<string> GetToken();
    }
}
