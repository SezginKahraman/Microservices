using Microservices.Shared.Core_3_1.Dtos;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Microservices.UI.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn();
        
        Task<OAuthTokenResponse> GetAccessTokenByRefreshToken();
        
        Task RevokeRefreshToken();
    }
}
