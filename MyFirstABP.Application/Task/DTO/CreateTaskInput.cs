using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP.DTO
{
    /// <summary>
    /// 创建Task的输入参数
    /// </summary>
    public class CreateTaskInput
    {
        /// <summary>
        /// 人的ID
        /// </summary>
        public int? AssignedPersonId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Required]
        public string Description { get; set; }
    }
}
