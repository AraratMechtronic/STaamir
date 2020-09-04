namespace Samane.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersianRoleName01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetRoles", "PersianRoelName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetRoles", "PersianRoelName");
        }
    }
}
