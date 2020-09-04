namespace Samane.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialIdentity2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ExpertInInstruments", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ExpertInInstruments");
        }
    }
}
