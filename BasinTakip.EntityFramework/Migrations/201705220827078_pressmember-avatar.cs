namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pressmemberavatar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PressMember", "Avatar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PressMember", "Avatar");
        }
    }
}
