namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactRecord", "ContactDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactRecord", "ContactDate");
        }
    }
}
