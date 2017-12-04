using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Abp.Auditing;
using Abp.Extensions;

namespace MyFirstABP.WebApi.Models
{
    /// <summary>
    /// ��������¼
    /// </summary>
    public class ExternalLoginModel
    {
        /// <summary>
        /// ����������
        /// </summary>
        [Required]
        public string LoginProvider { get; set; }
        /// <summary>
        /// �������˻�Ψһ��ʶ
        /// </summary>
        [Required]
        public string ProviderKey { get; set; }
    }
}