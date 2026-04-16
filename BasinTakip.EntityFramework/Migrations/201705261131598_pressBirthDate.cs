namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pressBirthDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PressMember", "BirthDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PressMember", "BirthDate", c => c.DateTime(nullable: false));
        }
    }
}
