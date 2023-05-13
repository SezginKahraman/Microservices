// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace Microservices.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes = {"catalog_fullpermission"}},//catalog api
            new ApiResource("resource_photo_stock"){Scopes = {"photo_stock_fullpermission"}},
            new ApiResource("resource_basket"){Scopes = {"basket_fullpermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        // This area is for the user proccesses.
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       // This check if the 'sub' keyword is exist in JWT payload. It is a must.
                       new IdentityResources.OpenId(), // represents id.
                       new IdentityResources.Profile(),
                       new IdentityResource(){Name = "roles", DisplayName = "Roles", Description = "User Roles", UserClaims = new []{"role"} // This parts map the 'role' part of jwt.
                       },

                //new IdentityResources.OpenId(),
                //new IdentityResources.Profile(),
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                //new ApiScope("scope1"),
                //new ApiScope("scope2"),
                new ApiScope("catalog_fullpermission","Catalog API için full erişim"), //permission type
                new ApiScope("photo_stock_fullpermission","Photo stock için full erişim"),
                new ApiScope("basket_fullpermission","Basket için full erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                //// m2m client credentials flow client
                //new Client
                //{
                //    ClientId = "m2m.client",
                //    ClientName = "Client Credentials Client",

                //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                //    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                //    AllowedScopes = { "scope1" }
                //},

                //// interactive client using code flow + pkce
                //new Client
                //{
                //    ClientId = "interactive",
                //    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                //    AllowedGrantTypes = GrantTypes.Code,

                //    RedirectUris = { "https://localhost:44300/signin-oidc" },
                //    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                //    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                //    AllowOfflineAccess = true,
                //    AllowedScopes = { "openid", "profile", "scope2" }
                //},
                new Client
                {
                    ClientName = "AspNetCoreMvc",
                    ClientId = "WebMvcClient",
                    ClientSecrets = {new Secret("secret".Sha512())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "catalog_fullpermission", "photo_stock_fullpermission", IdentityServerConstants.LocalApi.ScopeName }
                },
                new Client
                {
                    ClientName = "AspNetCoreMvc",
                    ClientId = "WebMvcClientForUser",
                    AllowOfflineAccess = true,
                    ClientSecrets = {new Secret("secret".Sha512())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = {"basket_full_permission", IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.OfflineAccess 
                    ,IdentityServerConstants.LocalApi.ScopeName, "roles"},
                    AccessTokenLifetime = 1*60*60,
                    RefreshTokenExpiration = TokenExpiration.Absolute, // when the expiration came, refresh absolutely cannot be used.
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds
                    ,RefreshTokenUsage = TokenUsage.ReUse // Refresh token can be used more than once.
                }
            };
    }
}