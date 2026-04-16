namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migContactRecordDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactRecord", "ContactDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ContactRecord", "ContactBeginDate");
            DropColumn("dbo.ContactRecord", "ContactEndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactRecord", "ContactEndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ContactRecord", "ContactBeginDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ContactRecord", "ContactDate");
        }
    }
}
