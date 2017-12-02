using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP.DTO
{
    /// <summary>
    /// 任务DTO
    /// </summary>
    public class TaskDto : EntityDto
    {
        /// <summary>
        /// 人ID
        /// </summary>
        public int? AssignedPersonId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public TaskState State { get; set; }
        /// <summary>
        /// 人类
        /// </summary>
        public MyClass MyProperty { get; set; }
    }

    /// <summary>
    ///我的泪
    /// </summary>
    public class MyClass
    {
        /// <summary>
        /// 整数
        /// </summary>
        public int MyProperty { get; set; }
        /// <summary>
        /// 字符
        /// </summary>
        public string MyProperty2 { get; set; }
    }
}
