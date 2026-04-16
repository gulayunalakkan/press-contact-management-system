namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PressMember", "EditionIds", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PressMember", "EditionIds");
        }
    }
}
