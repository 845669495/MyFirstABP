using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using MyFirstABP.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP.Event
{
    public abstract class EntityChangedHandlerBase<TEntity> :
        ITransientDependency,
        IEventHandler<EntityCreatedEventData<TEntity>>,
        IEventHandler<EntityDeletedEventData<TEntity>>,
        IEventHandler<EntityUpdatedEventData<TEntity>>
        where TEntity : class, IEntity<int>
    {
        public ICacheSyncService<int> CacheSyncService { get; set; }

        public virtual void HandleEvent(EntityCreatedEventData<TEntity> eventData)
        {
            CacheSyncService.Add(eventData.Entity);
        }

        public virtual void HandleEvent(EntityDeletedEventData<TEntity> eventData)
        {
            CacheSyncService.Remove<TEntity>(eventData.Entity.Id);
        }

        public virtual void HandleEvent(EntityUpdatedEventData<TEntity> eventData)
        {
            CacheSyncService.Update(eventData.Entity);
        }
    }

    public abstract class EntityChangedHandlerBase<TEntity, TPrimaryKey> :
       ITransientDependency,
       IEventHandler<EntityCreatedEventData<TEntity>>,
       IEventHandler<EntityDeletedEventData<TEntity>>,
       IEventHandler<EntityUpdatedEventData<TEntity>>
       where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly ICacheSyncService<TPrimaryKey> _cacheSyncService;
        public EntityChangedHandlerBase(ICacheSyncService<TPrimaryKey> cacheSyncService)
        {
            _cacheSyncService = cacheSyncService;
        }

        public virtual void HandleEvent(EntityCreatedEventData<TEntity> eventData)
        {
            _cacheSyncService.Add(eventData.Entity);
        }

        public virtual void HandleEvent(EntityDeletedEventData<TEntity> eventData)
        {
            _cacheSyncService.Remove<TEntity>(eventData.Entity.Id);
        }

        public virtual void HandleEvent(EntityUpdatedEventData<TEntity> eventData)
        {
            _cacheSyncService.Update(eventData.Entity);
        }
    }
}
