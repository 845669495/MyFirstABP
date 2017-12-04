using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Abp.Auditing;
using Abp.Extensions;

namespace MyFirstABP.WebApi.Models
{
    /// <summary>
    /// 第三方登录
    /// </summary>
    public class ExternalLoginModel
    {
        /// <summary>
        /// 第三方名称
        /// </summary>
        [Required]
        public string LoginProvider { get; set; }
        /// <summary>
        /// 第三方账户唯一标识
        /// </summary>
        [Required]
        public string ProviderKey { get; set; }
    }
}