using Abp.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFirstABP.EntityFramework;
using MyFirstABP.Authorization;
using Abp.Authorization.Roles;

namespace MyFirstABP.Migrations
{
    public class DefaultTenantRoleAndUserBuilder
    {
        private readonly MyFirstABPDbContext _context;

        public DefaultTenantRoleAndUserBuilder(MyFirstABPDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == "Default");
            if (defaultTenant == null)
            {
                defaultTenant = _context.Tenants.Add(new Tenant { TenancyName = "Default", Name = "Default" });
                _context.SaveChanges();
            }

            //Admin role for 'Default' tenant

            var adminRole = _context.Roles.FirstOrDefault(r => r.TenantId == defaultTenant.Id && r.Name == "Admin");
            if (adminRole == null)
            {
                Role role = new Role()
                {
                    TenantId = defaultTenant.Id,
                    Name = "Admin",
                    DisplayName = "Admin",
                    Permissions = new List<RolePermissionSetting>() { new RolePermissionSetting { Name = "AdminPermission", TenantId = defaultTenant.Id } }
                };
                adminRole = _context.Roles.Add(role);
                _context.SaveChanges();
            }

            //Admin for 'Default' tenant

            var adminUser = _context.Users.FirstOrDefault(u => u.TenantId == defaultTenant.Id && u.UserName == "admin");
            if (adminUser == null)
            {
                adminUser = _context.Users.Add(
                    new User
                    {
                        TenantId = defaultTenant.Id,
                        UserName = "admin",
                        Name = "System",
                        Surname = "Administrator",
                        EmailAddress = "admin@aspnetboilerplate.com",
                        IsEmailConfirmed = true,
                        Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                    });
                _context.SaveChanges();

                _context.UserRoles.Add(new UserRole(defaultTenant.Id, adminUser.Id, adminRole.Id));
                _context.SaveChanges();
            }
        }
    }
}
