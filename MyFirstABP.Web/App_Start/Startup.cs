using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using MyFirstABP.Web.App_Start;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace MyFirstABP.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 使应用程序能够使用cookie存储登录用户的信息
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            // 使用cookie临时存储关于用第三方登录提供程序登录的用户的信息
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}