namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migContactandEdition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactRecord", "ContactEndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactRecord", "ContactEndDate");
        }
    }
}
