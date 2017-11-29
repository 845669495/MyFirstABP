using Abp.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;

namespace MyFirstABP.Authorization
{
    public class RoleManager : AbpRoleManager<Role, User>
    {
        public RoleManager(AbpRoleStore<Role, User> store, IPermissionManager permissionManager, IRoleManagementConfig roleManagementConfig, ICacheManager cacheManager, IUnitOfWorkManager unitOfWorkManager) 
            : base(store, permissionManager, roleManagementConfig, cacheManager, unitOfWorkManager)
        {
        }
    }
}
