using Abp.Dependency;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP.Caching
{
    public class CacheSyncService<TPrimaryKey> : ICacheSyncService<TPrimaryKey>
    {
        public ICacheService CacheService { get; set; }

        public void Add<TEntity>(TEntity entity) where TEntity : class, IEntity<TPrimaryKey>
        {
            CacheService.Set(entity.Id, entity);
        }

        public void Remove<TEntity>(TPrimaryKey id) where TEntity : class, IEntity<TPrimaryKey>
        {
            CacheService.Remove<TPrimaryKey, TEntity>(id);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class, IEntity<TPrimaryKey>
        {
            CacheService.Set(entity.Id, entity);
        }
    }
}
