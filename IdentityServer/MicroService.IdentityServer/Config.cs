// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace MicroService.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catolog"){Scopes = {"catolog_fullpermission"}},
            new ApiResource("photo_stock_catolog"){Scopes = {"photo_stock_fullpermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)

        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catolog_fullpermission","Catolog Api için full erişim"),
                new ApiScope("photo_stock_fullpermission","PhotoStock Api için full erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientName = "Asp.Net.Core MVC",
                    ClientId = "WebMvcClient",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes= { "catolog_fullpermission", "photo_stock_fullpermission",IdentityServerConstants.LocalApi.ScopeName },
                    AllowOfflineAccess = true,
                },

                // interactive client using code flow + pkce
                //new Client
                //{
                //    //ClientId = "interactive",
                //    //ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                //    //AllowedGrantTypes = GrantTypes.Code,

                //    //RedirectUris = { "https://localhost:44300/signin-oidc" },
                //    //FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                //    //PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                //    //AllowOfflineAccess = true,
                //    //AllowedScopes = { "openid", "profile", "scope2" }
                //},
            };
    }
}