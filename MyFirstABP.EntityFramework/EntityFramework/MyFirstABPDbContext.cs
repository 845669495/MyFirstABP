using System.Data.Common;
using Abp.EntityFramework;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using MyFirstABP.Authorization;

namespace MyFirstABP.EntityFramework
{
    public class MyFirstABPDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        public virtual DbSet<Task> Tasks { get; set; }

        public virtual DbSet<Person> People { get; set; }

        //TODO: Define an IDbSet for each Entity...

        //Example:
        //public virtual IDbSet<User> Users { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public MyFirstABPDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in MyFirstABPDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of MyFirstABPDbContext since ABP automatically handles it.
         */
        public MyFirstABPDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public MyFirstABPDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public MyFirstABPDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
