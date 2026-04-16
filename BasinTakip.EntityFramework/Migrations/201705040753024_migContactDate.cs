namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migContactDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactRecord", "ContactBeginDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ContactRecord", "ContactEndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ContactRecord", "ContactDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactRecord", "ContactDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ContactRecord", "ContactEndDate");
            DropColumn("dbo.ContactRecord", "ContactBeginDate");
        }
    }
}
