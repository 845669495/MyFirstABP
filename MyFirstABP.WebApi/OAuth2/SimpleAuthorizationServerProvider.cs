using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Dependency;
using Abp.Localization;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using MyFirstABP.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP.OAuth2
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider, ITransientDependency
    {
        /// <summary>
        /// The _user manager
        /// </summary>
        private readonly LogInManager _userManager;

        public SimpleAuthorizationServerProvider(LogInManager userManager)
        {
            _userManager = userManager;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }
            var isValidClient = string.CompareOrdinal(clientId, "app") == 0 &&
                                string.CompareOrdinal(clientSecret, "app") == 0;
            if (isValidClient)
            {
                context.OwinContext.Set("as:client_id", clientId);
                context.Validated(clientId);
            }
            else
            {
                context.SetError("invalid client");
            }
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var tenantId = context.Request.Query["tenantId"];
            var result = await GetLoginResultAsync(context, context.UserName, context.Password, tenantId);
            if (result.Result == AbpLoginResultType.Success)
            {
                //var claimsIdentity = result.Identity;                
                var claimsIdentity = new ClaimsIdentity(result.Identity);
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                var ticket = new AuthenticationTicket(claimsIdentity, new AuthenticationProperties());
                context.Validated(ticket);
            }
        }

        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.OwinContext.Get<string>("as:client_id");
            var currentClient = context.ClientId;

            // enforce client binding of refresh token
            if (originalClient != currentClient)
            {
                context.Rejected();
                return;
            }

            // chance to change authentication ticket for refresh token requests
            var newId = new ClaimsIdentity(context.Ticket.Identity);
            newId.AddClaim(new Claim("newClaim", "refreshToken"));

            var newTicket = new AuthenticationTicket(newId, context.Ticket.Properties);
            context.Validated(newTicket);
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(OAuthGrantResourceOwnerCredentialsContext context, string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _userManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    CreateExceptionForFailedLoginAttempt(context, loginResult.Result, usernameOrEmailAddress, tenancyName);
                    return loginResult;
            }
        }

        private void CreateExceptionForFailedLoginAttempt(OAuthGrantResourceOwnerCredentialsContext context, AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    throw new ApplicationException("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    context.SetError("LoginFailed", "InvalidUserNameOrPassword");
                    break;
                case AbpLoginResultType.InvalidTenancyName:
                    context.SetError("LoginFailed", "ThereIsNoTenantDefinedWithName", tenancyName);
                    break;
                case AbpLoginResultType.TenantIsNotActive:
                    context.SetError("LoginFailed", "TenantIsNotActive", tenancyName);
                    break;
                case AbpLoginResultType.UserIsNotActive:
                    context.SetError("LoginFailed", "UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress);
                    break;
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    context.SetError("LoginFailed", "UserEmailIsNotConfirmedAndCanNotLogin");
                    break;
            }
        }
    }
}
