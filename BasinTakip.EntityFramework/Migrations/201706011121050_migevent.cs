namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migevent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "Url", c => c.String());
            AddColumn("dbo.EventSpecial", "Url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventSpecial", "Url");
            DropColumn("dbo.Event", "Url");
        }
    }
}
