namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migg2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PressMember", "MobilePhone", c => c.String(nullable: false));
            AlterColumn("dbo.PressMember", "CountryId", c => c.Int(nullable: false));
            AlterColumn("dbo.PressMember", "DistrictId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PressMember", "DistrictId", c => c.Int());
            AlterColumn("dbo.PressMember", "CountryId", c => c.Int());
            AlterColumn("dbo.PressMember", "MobilePhone", c => c.String());
        }
    }
}
