namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pressmembers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PressMember", "EditionId", c => c.Int(nullable: false));
            AddColumn("dbo.Event", "EventTypeName", c => c.String());
            AddColumn("dbo.Event", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "Discriminator");
            DropColumn("dbo.Event", "EventTypeName");
            DropColumn("dbo.PressMember", "EditionId");
        }
    }
}
