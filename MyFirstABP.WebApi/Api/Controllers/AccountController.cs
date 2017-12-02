using Abp.Authorization;
using Abp.UI;
using Abp.Web.Models;
using Abp.WebApi.Controllers;
using Microsoft.Extensions.Internal;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using MyFirstABP.Api.Models;
using MyFirstABP.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyFirstABP.Api.Controllers
{
    /// <summary>
    /// Api会员操作
    /// </summary>
    public class AccountController : AbpApiController
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        static AccountController()
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
        }

        private readonly LogInManager _logInManager;

        public AccountController(LogInManager logInManager)
        {
            _logInManager = logInManager;
        }

        /// <summary>
        /// 登录授权获取token
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResponse> Authenticate(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException("Invalid request!");
            }

            var loginResult = await _logInManager.LoginAsync(loginModel.UsernameOrEmailAddress, loginModel.Password, loginModel.TenancyName);

            if (loginResult.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Invalid UserName Or Password");
            }
            var ticket = new AuthenticationTicket(loginResult.Identity, new AuthenticationProperties());

            var currentUtc = new SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));

            return new AjaxResponse(OAuthBearerOptions.AccessTokenFormat.Protect(ticket));
        }
    }
}
