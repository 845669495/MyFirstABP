using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace MyFirstABP.EntityFramework.Repositories
{
    public abstract class MyFirstABPRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<MyFirstABPDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected MyFirstABPRepositoryBase(IDbContextProvider<MyFirstABPDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class MyFirstABPRepositoryBase<TEntity> : MyFirstABPRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected MyFirstABPRepositoryBase(IDbContextProvider<MyFirstABPDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
