using EntityFramework.DynamicFilters;
using System.Data.Entity.Migrations;

namespace MyFirstABP.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MyFirstABP.EntityFramework.MyFirstABPDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MyFirstABP";
        }

        protected override void Seed(MyFirstABP.EntityFramework.MyFirstABPDbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...

            context.DisableAllFilters();
            new DefaultTenantRoleAndUserBuilder(context).Build();
        }
    }
}
