namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactRecord", "EditionId", c => c.Int(nullable: false));
            AlterColumn("dbo.ContactRecord", "participationStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.ContactRecord", "accidentStatus", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactRecord", "accidentStatus", c => c.Boolean());
            AlterColumn("dbo.ContactRecord", "participationStatus", c => c.Boolean(nullable: false));
            DropColumn("dbo.ContactRecord", "EditionId");
        }
    }
}
