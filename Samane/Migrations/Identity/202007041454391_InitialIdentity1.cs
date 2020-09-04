namespace Samane.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialIdentity1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "HospitalName", c => c.String());
            DropColumn("dbo.AspNetUsers", "ExpertInInstruments");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ExpertInInstruments", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "HospitalName");
        }
    }
}
