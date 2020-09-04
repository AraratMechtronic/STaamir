namespace Samane.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialIdentity31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NameAndLastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "NameAndLastName");
        }
    }
}
