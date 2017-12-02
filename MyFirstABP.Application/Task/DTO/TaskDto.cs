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
    }
}
