namespace Samane.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialIdentity81 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TellNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "CellNumber", c => c.String());
            AlterColumn("dbo.AspNetUsers", "userRole", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "NameAndLastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "NameAndLastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "userRole", c => c.String());
            DropColumn("dbo.AspNetUsers", "CellNumber");
            DropColumn("dbo.AspNetUsers", "TellNumber");
        }
    }
}
