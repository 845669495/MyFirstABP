using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP.DTO
{
    public class UpdateTaskInput : ICustomValidate
    {
        [Range(1, long.MaxValue)]
        public long TaskId { get; set; }

        public TaskState? State { get; set; }

        public int? AssignedPersonId { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (AssignedPersonId == null && State == null)
            {
                context.Results.Add(new ValidationResult("AssignedPersonId和State不能同时为空!", new[] { "AssignedPersonId", "State" }));
            }
        }
    }
}
