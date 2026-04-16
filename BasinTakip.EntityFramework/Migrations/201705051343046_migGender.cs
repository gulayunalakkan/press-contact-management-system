namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migGender : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PressMember", "Gender", c => c.String(nullable: false, maxLength: 512));
            DropColumn("dbo.PressMember", "GenderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PressMember", "GenderId", c => c.Int(nullable: false));
            DropColumn("dbo.PressMember", "Gender");
        }
    }
}
