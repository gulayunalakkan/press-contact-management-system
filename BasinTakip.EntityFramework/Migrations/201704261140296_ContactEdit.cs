namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactRecord", "EditionTypeName", c => c.String());
            AddColumn("dbo.ContactRecord", "PressMemberName", c => c.String());
            AddColumn("dbo.ContactRecord", "ContactTypeName", c => c.String());
            AddColumn("dbo.ContactRecord", "LcvName", c => c.String());
            AddColumn("dbo.ContactRecord", "participationStatusName", c => c.String());
            AddColumn("dbo.ContactRecord", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactRecord", "Discriminator");
            DropColumn("dbo.ContactRecord", "participationStatusName");
            DropColumn("dbo.ContactRecord", "LcvName");
            DropColumn("dbo.ContactRecord", "ContactTypeName");
            DropColumn("dbo.ContactRecord", "PressMemberName");
            DropColumn("dbo.ContactRecord", "EditionTypeName");
        }
    }
}
