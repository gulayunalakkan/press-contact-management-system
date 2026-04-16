namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class miglog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoginLog", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoginLog", "Description");
        }
    }
}
