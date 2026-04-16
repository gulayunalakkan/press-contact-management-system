namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migRevize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "EventNotes", c => c.String(nullable: false, maxLength: 500));
            DropColumn("dbo.PressMember", "OfficePhone");
            DropColumn("dbo.PressMember", "Gender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PressMember", "Gender", c => c.String(maxLength: 512));
            AddColumn("dbo.PressMember", "OfficePhone", c => c.String());
            DropColumn("dbo.Event", "EventNotes");
        }
    }
}
