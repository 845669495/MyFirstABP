using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Abp.Auditing;
using Abp.Extensions;

namespace MyFirstABP.WebApi.Models
{
    /// <summary>
    /// ע��ģ��
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// �û���
        /// </summary>
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}