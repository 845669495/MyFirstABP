using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP.DTO
{
    /// <summary>
    /// 获取Task的输入参数
    /// </summary>
    public class GetTasksInput
    {
        public int? AssignedPersonId { get; set; }

        public TaskState? State { get; set; }
    }
}
