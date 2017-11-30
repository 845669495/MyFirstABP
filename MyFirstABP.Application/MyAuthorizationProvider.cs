using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP
{
    public class MyAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var administration = context.CreatePermission("AdminPermission");

            //var userManagement = administration.CreateChildPermission("AdminPermission.UserManagement");
            //var roleManagement = administration.CreateChildPermission("AdminPermission.RoleManagement");
            //userManagement.CreateChildPermission("AdminPermission.UserManagement.CreateUser");

        }
    }
}
