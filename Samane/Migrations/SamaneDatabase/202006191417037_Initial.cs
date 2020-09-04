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
                        HospitalName = c.String(nullable: false, maxLength: 128),
                        Province = c.String(nullable: false),
                        City = c.String(nullable: false),
                        InChargePerson = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.HospitalName);
            
            CreateTable(
                "dbo.Instruments",
                c => new
                    {
                        InstrumentId = c.Int(nullable: false),
                        Category = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        SerialNo = c.String(nullable: false),
                        HospitalName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.InstrumentId)
                .ForeignKey("dbo.Hospitals", t => t.HospitalName)
                .Index(t => t.HospitalName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Instruments", "HospitalName", "dbo.Hospitals");
            DropIndex("dbo.Instruments", new[] { "HospitalName" });
            DropTable("dbo.Instruments");
            DropTable("dbo.Hospitals");
        }
    }
}
