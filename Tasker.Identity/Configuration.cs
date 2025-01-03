﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;
using System.Security.Claims;

namespace Tasker.Identity
{
    public class Configuration
    {

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope> { new ApiScope("TaskerWebAPI", "Web API") };

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),

			new IdentityResource("roles", "roles", new List<string> { ClaimTypes.Role })
		};

        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource("TaskerWebAPI", "WebAPI", new [] {JwtClaimTypes.Name})
            {
                Scopes = {"TaskerWebAPI"}
            }
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
			new Client
   {
	   ClientId = "tasker-backend",
	   AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
	   RequireClientSecret = false,
	   RequirePkce = false,
			  RedirectUris =
				{
				   "http://localhost:7138"
				},
				AllowedCorsOrigins =
				{
					"http://localhost:7138"
				},
				PostLogoutRedirectUris =
				{
					 "http://localhost:7138"
				},
	   AllowedScopes =
	   {
		   IdentityServerConstants.StandardScopes.OpenId,
		   IdentityServerConstants.StandardScopes.Profile,
		   "TaskerWebAPI",
		   "roles"
	   },
	   AllowAccessTokensViaBrowser = true
   },
			 new Client
   {
	   ClientId = "tasker-worker",
	   AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
	   RequireClientSecret = false,
	   RequirePkce = false,
			  RedirectUris =
				{
				   "http://localhost:7138"
				},
				AllowedCorsOrigins =
				{
					"http://localhost:7138"
				},
				PostLogoutRedirectUris =
				{
					 "http://localhost:7138"
				},
	   AllowedScopes =
	   {
		   IdentityServerConstants.StandardScopes.OpenId,
		   IdentityServerConstants.StandardScopes.Profile,
		   "TaskerWebAPI",
           "roles"
	   },
	   AllowAccessTokensViaBrowser = true
   }
		};
    }
}
