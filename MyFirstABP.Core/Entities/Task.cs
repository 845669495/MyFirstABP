using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP
{
    [Table("Tasks")]
    public class TaskEntity : Entity<long>
    {
        public virtual Person AssignedPerson { get; set; }

        public int? AssignedPersonId { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public TaskState State { get; set; }

        public TaskEntity()
        {
            CreationTime = DateTime.Now;
            State = TaskState.Active;
        }
    }
    public enum TaskState
    {
        Active
    }
}
