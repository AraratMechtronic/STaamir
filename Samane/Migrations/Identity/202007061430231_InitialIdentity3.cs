namespace Samane.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialIdentity3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: true, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "ExpertInInstruments", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "ExpertInInstruments", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Discriminator");
        }
    }
}
