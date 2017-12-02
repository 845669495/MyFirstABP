using Abp.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstABP.Authorization
{
    public class RoleStore : AbpRoleStore<Role, User>
    {
        public RoleStore(IRepository<Role> roleRepository, IRepository<UserRole, long> userRoleRepository, IRepository<RolePermissionSetting, long> rolePermissionSettingRepository) 
            : base(roleRepository, userRoleRepository, rolePermissionSettingRepository)
        {
        }
    }
}
