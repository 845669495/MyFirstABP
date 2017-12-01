using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFirstABP.Caching;

namespace MyFirstABP.Event
{
    public class TaskChangedHandler : EntityChangedHandlerBase<Task, long>
    {
        public TaskChangedHandler(ICacheSyncService<long> cacheSyncService) : base(cacheSyncService)
        {
        }
    }
}
