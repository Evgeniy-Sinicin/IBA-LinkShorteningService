using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;

namespace LinkShorteningService.IdentityServer.Web
{
	public static class Config
	{
		public static IEnumerable<ApiScope> ApiScopes
		{
			get => new List<ApiScope>
				{
					new ApiScope("LSS_api.CRUD", "LinkShorteningServiceApi CRUD access", new [] { ClaimTypes.Role })
				};
		}

		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
				new ApiResource("links", "Links API", new [] { ClaimTypes.Role })
				{
					Scopes = { "LSS_api.CRUD" }
				}
			};
		}

		public static IEnumerable<IdentityResource> IdentityResources
		{
			get => new List<IdentityResource>
				{
					new IdentityResources.OpenId(),
					new IdentityResources.Email(),
					new IdentityResources.Profile()
				};
		}

		public static IEnumerable<Client> GetClients(IConfiguration configuration)
		{
			return new List<Client>
				{
					new Client
					{
						ClientId = "angular_spa",
						ClientName = "Angular SPA client",
						AllowedGrantTypes = GrantTypes.Code,
						AllowedScopes = new List<string>
						{ 
							IdentityServerConstants.StandardScopes.OpenId,
							IdentityServerConstants.StandardScopes.Profile,
							IdentityServerConstants.StandardScopes.Email,
							"LSS_api.CRUD"
						},

						RequireClientSecret = false,
						AlwaysIncludeUserClaimsInIdToken = true,
						AlwaysSendClientClaims = true,
						AccessTokenLifetime = configuration.GetValue<int>("TokenLifeTime"),

						RedirectUris = { configuration.GetValue<string>("AngularRedirectUri") },
						PostLogoutRedirectUris = { configuration.GetValue<string>("AngularPostLogoutRedirectUri") },
						AllowedCorsOrigins = { configuration.GetValue<string>("AngularAllowedOrigin") }
					}
				};
		}
	}
}
