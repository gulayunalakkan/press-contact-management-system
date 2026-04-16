namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migedit1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PressMember", "TaskId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PressMember", "TaskId", c => c.Int(nullable: false));
        }
    }
}
