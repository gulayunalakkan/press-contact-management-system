namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migpressRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PressMember", "OfficePhone", c => c.String());
            AlterColumn("dbo.PressMember", "MobilePhone", c => c.String());
            AlterColumn("dbo.PressMember", "Fax", c => c.String());
            AlterColumn("dbo.PressMember", "Email2", c => c.String());
            AlterColumn("dbo.PressMember", "Adress", c => c.String());
            AlterColumn("dbo.PressMember", "Notes", c => c.String());
            AlterColumn("dbo.PressMember", "About", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PressMember", "About", c => c.String(nullable: false));
            AlterColumn("dbo.PressMember", "Notes", c => c.String(nullable: false));
            AlterColumn("dbo.PressMember", "Adress", c => c.String(nullable: false));
            AlterColumn("dbo.PressMember", "Email2", c => c.String(nullable: false));
            AlterColumn("dbo.PressMember", "Fax", c => c.String(nullable: false));
            AlterColumn("dbo.PressMember", "MobilePhone", c => c.String(nullable: false));
            AlterColumn("dbo.PressMember", "OfficePhone", c => c.String(nullable: false));
        }
    }
}
