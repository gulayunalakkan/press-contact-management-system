namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class picklist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PickList", "PicListId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PickList", "PicListId");
        }
    }
}
