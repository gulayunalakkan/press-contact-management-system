namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PressMember", "Path1", c => c.String());
            AddColumn("dbo.PressMember", "Path2", c => c.String());
            AddColumn("dbo.PressMember", "Path3", c => c.String());
            AddColumn("dbo.PressMember", "Path4", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PressMember", "Path4");
            DropColumn("dbo.PressMember", "Path3");
            DropColumn("dbo.PressMember", "Path2");
            DropColumn("dbo.PressMember", "Path1");
        }
    }
}
