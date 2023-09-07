using Microservices.Shared.Core_3_1.Dtos;
using Microservices.UI.Models;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Microservices.UI.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SignInInput signInInput);
        
        Task<OAuthTokenResponse> GetAccessTokenByRefreshToken();
        
        Task RevokeRefreshToken();
    }
}
