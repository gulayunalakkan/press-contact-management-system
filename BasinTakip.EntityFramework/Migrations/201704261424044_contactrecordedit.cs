namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactrecordedit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ContactRecord", "ContactDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactRecord", "ContactDate", c => c.DateTime(nullable: false));
        }
    }
}
