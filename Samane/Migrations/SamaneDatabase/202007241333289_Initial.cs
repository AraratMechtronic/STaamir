namespace Samane.Migrations.SamaneDatabase
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hospitals",
                c => new
                    {
                        UserNamee = c.String(nullable: false, maxLength: 128),
                        HospitalName = c.String(nullable: false),
                        Province = c.String(nullable: false),
                        City = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        InChargePerson = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserNamee);
            
            CreateTable(
                "dbo.Instruments",
                c => new
                    {
                        InstrumentId = c.Int(nullable: false, identity: true),
                        Category = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        SerialNo = c.String(nullable: false),
                        UserName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.InstrumentId)
                .ForeignKey("dbo.Hospitals", t => t.UserName)
                .Index(t => t.UserName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Instruments", "UserName", "dbo.Hospitals");
            DropIndex("dbo.Instruments", new[] { "UserName" });
            DropTable("dbo.Instruments");
            DropTable("dbo.Hospitals");
        }
    }
}
