using MyFirstABP.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP.DTO
{

    /// <summary>
    /// 获取任务输出参数
    /// </summary>
    public class GetTasksOutput
    {
        /// <summary>
        /// 任务集合
        /// </summary>
        public List<TaskDto> Tasks { get; set; }
    }
}
