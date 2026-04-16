namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migUpdate2311 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PressMember", "EditionNames", c => c.String());
            DropColumn("dbo.PressMember", "editionlist");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PressMember", "editionlist", c => c.String());
            DropColumn("dbo.PressMember", "EditionNames");
        }
    }
}
