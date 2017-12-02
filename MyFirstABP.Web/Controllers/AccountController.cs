using Abp.Authorization;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using MyFirstABP.Authorization;
using MyFirstABP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyFirstABP.Web.Controllers
{
    /// <summary>
    /// Web会员操作
    /// </summary>
    public class AccountController : MyFirstABPControllerBase
    {
        private readonly UserManager _userManager;

        private readonly LogInManager _logInManager;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(UserManager userManager,LogInManager logInManager)
        {
            _userManager = userManager;
            _logInManager = logInManager;
        }

        public ActionResult Login(string returnUrl = "")
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "")
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException("Your form is invalid!");
            }

            var loginResult = await _logInManager.LoginAsync(
                loginModel.UsernameOrEmailAddress,
                loginModel.Password,
                loginModel.TenancyName
                );

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    break;
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    throw new UserFriendlyException("Invalid user name or password!");
                case AbpLoginResultType.InvalidTenancyName:
                    throw new UserFriendlyException("No tenant with name: " + loginModel.TenancyName);
                case AbpLoginResultType.TenantIsNotActive:
                    throw new UserFriendlyException("Tenant is not active: " + loginModel.TenancyName);
                case AbpLoginResultType.UserIsNotActive:
                    throw new UserFriendlyException("User is not active: " + loginModel.UsernameOrEmailAddress);
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    throw new UserFriendlyException("Your email address is not confirmed!");
                default: //Can not fall to default for now. But other result types can be added in the future and we may forget to handle it
                    throw new UserFriendlyException("Unknown problem with login: " + loginResult.Result);
            }

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = loginModel.RememberMe }, loginResult.Identity);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            return Json(new AjaxResponse { TargetUrl = returnUrl });
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}