using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Duende.Services.IdentityNew
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>()
        {
            new ApiScope("mango", "Mango server"),
            new ApiScope("read", "Read your data"),
            new ApiScope("write", "Write your data"),
            new ApiScope("delete", "Delete your data")
        };

        public static IEnumerable<Client> Clients => new List<Client>()
        {
            new Client()
            {
                ClientId = "client",
                ClientSecrets = {new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"read", "write", "profile"}
            },

            new Client()
            {
                ClientId = "mango",
                ClientSecrets = {new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = {"https://localhost:44344/signin-oidc" , "https://localhost:7141/signin-oidc", "http://localhost:5269/signin-oidc"},
                PostLogoutRedirectUris= {"https://localhost:44344/signout-callback-oidc" },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile,
                    "mango"
                }
            }
        };
    }
}
