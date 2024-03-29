﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DevIO.IntegrationTests.Setups.Auth
{
    public class BypassPolicyEvaluator : IPolicyEvaluator
    {
        public virtual async Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            // Always pass authentication
            var principal = new ClaimsPrincipal();

            principal.AddIdentity(new ClaimsIdentity(Array.Empty<Claim>(), "FakeScheme"));

            return await Task.FromResult(AuthenticateResult.Success(
                new AuthenticationTicket(principal, new AuthenticationProperties(), "FakeScheme"))
            );
        }

        public virtual async Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object resource)
        {
            // Always pass authorization
            return await Task.FromResult(PolicyAuthorizationResult.Success());
        }
    }
}
