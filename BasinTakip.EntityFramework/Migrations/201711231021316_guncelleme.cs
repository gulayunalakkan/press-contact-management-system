namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guncelleme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PressMember", "editionlist", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PressMember", "editionlist");
        }
    }
}
