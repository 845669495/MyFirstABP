using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using Microsoft.AspNet.Identity;
using Abp.IdentityFramework;

namespace MyFirstABP.Authorization
{
    public class LogInManager : AbpLogInManager<Tenant, Role, User>
    {
        public IRepository<UserLogin,long> UserLoginRepository { get; set; }
        public LogInManager(UserManager userManager, IMultiTenancyConfig multiTenancyConfig, IRepository<Tenant> tenantRepository, IUnitOfWorkManager unitOfWorkManager, ISettingManager settingManager, IRepository<UserLoginAttempt, long> userLoginAttemptRepository, IUserManagementConfig userManagementConfig, IIocResolver iocResolver, RoleManager roleManager) 
            : base(userManager, multiTenancyConfig, tenantRepository, unitOfWorkManager, settingManager, userLoginAttemptRepository, userManagementConfig, iocResolver, roleManager)
        {
        }

        /// <summary>
        /// 账户密码注册
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<AbpLoginResult<Tenant, User>> Register(string userName, string password)
        {
            var user = new User
            {
                TenantId = 1,
                Name = userName,
                Surname = userName,
                EmailAddress = userName,
                UserName = userName,
                IsActive = true
            };

            user.Password = new PasswordHasher().HashPassword(password);

            var role = RoleManager.Roles.Single(r => r.IsDefault);
            user.Roles = new List<UserRole>() { new UserRole(1, user.Id, role.Id) };

            (await UserManager.CreateAsync(user)).CheckErrors();
            await UnitOfWorkManager.Current.SaveChangesAsync();

            return await LoginAsync(userName, password);
        }

        [UnitOfWork]
        public virtual async Task<AbpLoginResult<Tenant, User>> ExternalRegisterOrBind(string userName, string loginProvider, string providerKey)
        {
            if (UserLoginRepository.Count(p => p.LoginProvider == loginProvider && p.ProviderKey == providerKey) > 0)
                throw new Exception("发生异常，第三方账号已存在");
            var user = this.UserManager.Users.SingleOrDefault(p => p.UserName == userName);
            if (user == null)
            {
                user = new User
                {
                    TenantId = 1,
                    Name = userName,
                    Surname = userName,
                    EmailAddress = userName,
                    UserName = userName,
                    IsActive = true,
                    Logins = new List<UserLogin>()
                };
                string password = User.CreateRandomPassword();
                user.Password = new PasswordHasher().HashPassword(password);
                var role = RoleManager.Roles.Single(r => r.IsDefault);
                user.Roles = new List<UserRole>() { new UserRole(1, user.Id, role.Id) };
                (await UserManager.CreateAsync(user)).CheckErrors();
            }

            if (user.Logins != null && user.Logins.Count > 0)
                throw new Exception("发生异常，该账号已经绑定第三方账号");

            user.Logins.Add(new UserLogin { LoginProvider = loginProvider, ProviderKey = providerKey, TenantId = 1 });
            await UnitOfWorkManager.Current.SaveChangesAsync();

            return await LoginAsync(new UserLoginInfo(loginProvider, providerKey));
        }
    }
}
