using Microservices.UI.Models;

namespace Microservices.UI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
