namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migContactRecordNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactRecord", "participationStatus", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactRecord", "participationStatus", c => c.Int(nullable: false));
        }
    }
}
