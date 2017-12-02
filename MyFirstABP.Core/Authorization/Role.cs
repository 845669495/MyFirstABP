using Abp.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP.Authorization
{
    [Table("Role")]
    public class Role : AbpRole<User>
    {
    }
}
