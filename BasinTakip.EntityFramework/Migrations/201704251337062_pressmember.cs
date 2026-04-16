namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pressmember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PressMember", "GenderId", c => c.Int(nullable: false));
            DropColumn("dbo.PressMember", "Gender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PressMember", "Gender", c => c.Boolean(nullable: false));
            DropColumn("dbo.PressMember", "GenderId");
        }
    }
}
