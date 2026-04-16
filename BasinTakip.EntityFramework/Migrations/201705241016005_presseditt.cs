namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class presseditt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PressMember", "Adress", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PressMember", "Adress", c => c.String());
        }
    }
}
