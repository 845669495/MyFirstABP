using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.Collections.Extensions;
using System.Data.Entity;
using MyFirstABP.IRepositories;

namespace MyFirstABP.EntityFramework.Repositories
{
    public class TaskRepository : MyFirstABPRepositoryBase<TaskEntity, long>, ITaskRepository
    {
        public TaskRepository(IDbContextProvider<MyFirstABPDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<TaskEntity> GetAllWithPeople(int? assignedPersonId, TaskState? state)
        {
            //在仓储方法中，不用处理数据库连接、DbContext和数据事务，ABP框架会自动处理。
            var query = GetAll();

            if (assignedPersonId.HasValue)
            {
                query = query.Where(p => p.AssignedPersonId == assignedPersonId);
            }
            if (state.HasValue)
            {
                query = query.Where(p => p.State == state);
            }

            return query.OrderByDescending(p => p.CreationTime).Include(p => p.AssignedPerson).ToList();
        }
    }
}
