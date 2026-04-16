namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editss : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PressMember", "Block", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PressMember", "Block", c => c.Boolean(nullable: false));
        }
    }
}
