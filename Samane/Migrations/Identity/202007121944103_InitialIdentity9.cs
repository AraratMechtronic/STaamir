namespace Samane.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialIdentity9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "YearsOfWork", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "YearsOfWork");
        }
    }
}
