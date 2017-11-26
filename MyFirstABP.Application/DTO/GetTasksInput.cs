using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP.DTO
{
    public class GetTasksInput
    {
        public int? AssignedPersonId { get; set; }

        public TaskState? State { get; set; }
    }
}
