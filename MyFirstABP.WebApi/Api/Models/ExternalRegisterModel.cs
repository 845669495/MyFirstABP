using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Abp.Auditing;
using Abp.Extensions;

namespace MyFirstABP.WebApi.Models
{
    /// <summary>
    /// ������ע��
    /// </summary>
    public class ExternalRegisterModel
    {
        /// <summary>
        /// �û���
        /// </summary>
        [Required]
        public string UserName { get; set; }
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