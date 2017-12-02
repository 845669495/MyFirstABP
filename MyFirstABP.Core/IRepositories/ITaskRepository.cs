using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP.IRepositories
{
    public interface ITaskRepository : IRepository<TaskEntity, long>
    {
        List<TaskEntity> GetAllWithPeople(int? assignedPersonId, TaskState? state);
    }
}
