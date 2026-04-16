namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editeventCalendareventSpecial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventSpecial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            Sql(@"
IF OBJECT_ID('dbo.EventCalendar','U') IS NOT NULL
    DROP TABLE dbo.EventCalendar;
");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EventCalendar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        start = c.DateTime(nullable: false),
                        end = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.EventSpecial");
        }
    }
}
