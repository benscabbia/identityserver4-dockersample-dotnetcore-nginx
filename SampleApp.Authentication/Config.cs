using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace SampleApp.Authentication
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "SampleApp.API",
                    DisplayName = "SampleApp API",
                    ApiSecrets =
                    {
                        new Secret("apisecret".Sha256())
                    },
                    Scopes = new []
                    {
                        new Scope("SampleApp.API", "SampleApp API")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "SampleApp.Mobile",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "SampleApp.API"
                    },
                    AllowOfflineAccess = DisableRefreshTokens(),
                    // The SampleApp.Mobile app is not a trusted client - it's unable to store client secret     
                    RequireClientSecret = false,
                    AccessTokenLifetime = OneMonth(),
                }
            };
        }

        private static int OneMonth() => 2592000;
        private static bool DisableRefreshTokens() => false;

        public static IEnumerable<TestUser> GetUsers()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "ben",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password"
                }
            };
        }
    }
}
