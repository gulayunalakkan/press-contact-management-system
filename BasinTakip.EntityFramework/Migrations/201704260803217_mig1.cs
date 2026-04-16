namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Edition", "EditionTypeName");
            DropColumn("dbo.Edition", "Discriminator");
            DropColumn("dbo.Event", "EventTypeName");
            DropColumn("dbo.Event", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Event", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Event", "EventTypeName", c => c.String());
            AddColumn("dbo.Edition", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Edition", "EditionTypeName", c => c.String());
        }
    }
}
