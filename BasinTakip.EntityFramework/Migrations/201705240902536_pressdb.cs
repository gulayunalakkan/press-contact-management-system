namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pressdb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PressMember", "Gender", c => c.String(maxLength: 512));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PressMember", "Gender", c => c.String(nullable: false, maxLength: 512));
        }
    }
}
