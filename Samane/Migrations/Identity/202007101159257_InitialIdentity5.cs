namespace Samane.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialIdentity5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "userRole", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "userRole");
        }
    }
}
