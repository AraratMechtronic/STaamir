namespace Samane.Migrations.SamaneDatabase
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Instruments", "UserName", "dbo.Hospitals");
            DropIndex("dbo.Instruments", new[] { "UserName" });
            AlterColumn("dbo.Instruments", "UserName", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Instruments", "UserName");
            AddForeignKey("dbo.Instruments", "UserName", "dbo.Hospitals", "UserNamee", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Instruments", "UserName", "dbo.Hospitals");
            DropIndex("dbo.Instruments", new[] { "UserName" });
            AlterColumn("dbo.Instruments", "UserName", c => c.String(maxLength: 128));
            CreateIndex("dbo.Instruments", "UserName");
            AddForeignKey("dbo.Instruments", "UserName", "dbo.Hospitals", "UserNamee");
        }
    }
}
