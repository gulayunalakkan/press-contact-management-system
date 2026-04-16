namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicle", "ModelDate", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicle", "ModelDate", c => c.String(nullable: false));
        }
    }
}
