namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pressRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PressMember", "TaskId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PressMember", "TaskId", c => c.Int());
        }
    }
}
