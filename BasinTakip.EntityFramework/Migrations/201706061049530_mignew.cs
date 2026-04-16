namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mignew : DbMigration
    {
        public override void Up()
        {
            Sql(@"
IF OBJECT_ID('dbo.DataHistory','U') IS NOT NULL
    DROP TABLE dbo.DataHistory;
");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.DataHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityType = c.String(maxLength: 128),
                        EntityId = c.String(maxLength: 128),
                        Data = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedMemberId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
