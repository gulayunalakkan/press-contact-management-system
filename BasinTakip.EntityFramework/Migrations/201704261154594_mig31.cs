namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig31 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ContactRecord", "EditionTypeName");
            DropColumn("dbo.ContactRecord", "PressMemberName");
            DropColumn("dbo.ContactRecord", "ContactTypeName");
            DropColumn("dbo.ContactRecord", "LcvName");
            DropColumn("dbo.ContactRecord", "participationStatusName");
            DropColumn("dbo.ContactRecord", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactRecord", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ContactRecord", "participationStatusName", c => c.String());
            AddColumn("dbo.ContactRecord", "LcvName", c => c.String());
            AddColumn("dbo.ContactRecord", "ContactTypeName", c => c.String());
            AddColumn("dbo.ContactRecord", "PressMemberName", c => c.String());
            AddColumn("dbo.ContactRecord", "EditionTypeName", c => c.String());
        }
    }
}
