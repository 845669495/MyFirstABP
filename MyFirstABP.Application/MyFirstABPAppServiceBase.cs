using Abp.Application.Services;
using Abp.Runtime.Session;
using Microsoft.AspNet.Identity;
using MyFirstABP.Authorization;
using System;
using System.Threading.Tasks;

namespace MyFirstABP
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class MyFirstABPAppServiceBase : ApplicationService
    {
        public UserManager UserManager { get; set; }

        protected MyFirstABPAppServiceBase()
        {
            LocalizationSourceName = MyFirstABPConsts.LocalizationSourceName;
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual User GetCurrentUser()
        {
            var user = UserManager.FindById(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }
    }
}