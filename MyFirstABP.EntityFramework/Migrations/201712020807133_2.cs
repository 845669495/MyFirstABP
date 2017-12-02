namespace MyFirstABP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AbpRoles", newName: "Role");
            RenameTable(name: "dbo.AbpUsers", newName: "User");
            RenameTable(name: "dbo.AbpTenants", newName: "Tenant");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Tenant", newName: "AbpTenants");
            RenameTable(name: "dbo.User", newName: "AbpUsers");
            RenameTable(name: "dbo.Role", newName: "AbpRoles");
        }
    }
}
