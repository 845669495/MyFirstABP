using Abp.Dependency;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP.Caching
{
    public interface ICacheSyncService<TPrimaryKey>
    {
        void Add<TEntity>(TEntity entity) where TEntity : class, IEntity<TPrimaryKey>;
        void Remove<TEntity>(TPrimaryKey id) where TEntity : class, IEntity<TPrimaryKey>;
        void Update<TEntity>(TEntity entity) where TEntity : class, IEntity<TPrimaryKey>;
    }
}
