using Abp.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP.Authorization
{
    [Table("User")]
    public class User : AbpUser<User>
    {
    }
}
