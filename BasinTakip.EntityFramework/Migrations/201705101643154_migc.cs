namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactRecord", "Notes", c => c.String());
            AlterColumn("dbo.ContactRecord", "LcvId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactRecord", "LcvId", c => c.Int(nullable: false));
            AlterColumn("dbo.ContactRecord", "Notes", c => c.String(nullable: false));
        }
    }
}
