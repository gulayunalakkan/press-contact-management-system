namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migpress : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PressMember", "CountryId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PressMember", "CountryId", c => c.Int(nullable: false));
        }
    }
}
