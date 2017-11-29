﻿using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;

namespace MyFirstABP.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        protected PermissionChecker(AbpUserManager<Role, User> userManager) : base(userManager)
        {
        }
    }
}
