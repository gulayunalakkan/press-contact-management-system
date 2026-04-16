namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migContactKind : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactRecord", "ContactKindId", c => c.Int(nullable: false));
            AlterColumn("dbo.ContactRecord", "ContactTypeSubId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactRecord", "ContactTypeSubId", c => c.Int(nullable: false));
            DropColumn("dbo.ContactRecord", "ContactKindId");
        }
    }
}
