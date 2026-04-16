namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PressMember", "EditionId", c => c.Int());
            DropColumn("dbo.PressMember", "FirmId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PressMember", "FirmId", c => c.Int());
            AlterColumn("dbo.PressMember", "EditionId", c => c.Int(nullable: false));
        }
    }
}
