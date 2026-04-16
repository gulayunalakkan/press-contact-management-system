namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pressmembercountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PressMember", "CountryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PressMember", "CountryId");
        }
    }
}
