using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Web.Models;
using Abp.WebApi.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Internal;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using MyFirstABP.Api.Models;
using MyFirstABP.Authorization;
using MyFirstABP.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
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

            var loginResult = await _logInManager.LoginAsync(loginModel.UserName, loginModel.Password);
            string token = GetToken(loginResult);
            return new AjaxResponse(token);
        }

        /// <summary>
        /// 第三方登录授权
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public async Task<AjaxResponse> ExternalAuthenticate(ExternalLoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException("Invalid request!");
            }

            var loginResult = await _logInManager.LoginAsync(new UserLoginInfo(loginModel.LoginProvider, loginModel.ProviderKey));
            string token = GetToken(loginResult);
            return new AjaxResponse(token);
        }

        /// <summary>
        /// 账号密码注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResponse> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException("Invalid request!");
            }

            var loginResult = await _logInManager.Register(model.UserName, model.Password);
            string token = GetToken(loginResult);
            return new AjaxResponse(token);
        }

        /// <summary>
        /// 第三方注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AjaxResponse> ExternalRegisterOrBind(ExternalRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException("Invalid request!");
            }

            var loginResult = await _logInManager.ExternalRegisterOrBind(model.UserName, model.LoginProvider, model.ProviderKey);
            string token = GetToken(loginResult);
            return new AjaxResponse(token);
        }


        private string GetToken(AbpLoginResult<Tenant, User> loginResult)
        {
            if (loginResult.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("登录或注册失败。结果：" + loginResult.Result);
            }

            var ticket = new AuthenticationTicket(loginResult.Identity, new AuthenticationProperties());

            var currentUtc = new SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));

            return OAuthBearerOptions.AccessTokenFormat.Protect(ticket);
        }
    }
}
