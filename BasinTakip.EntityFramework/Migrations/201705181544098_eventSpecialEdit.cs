namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventSpecialEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventSpecial", "EventPlace", c => c.String());
            AddColumn("dbo.EventSpecial", "EventAdress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventSpecial", "EventAdress");
            DropColumn("dbo.EventSpecial", "EventPlace");
        }
    }
}
