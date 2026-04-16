namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactRecordedit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactRecord", "ContactTypeSubId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactRecord", "ContactTypeSubId");
        }
    }
}
