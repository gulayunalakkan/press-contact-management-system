namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataHistory2 : DbMigration
    {
        public override void Up()
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
                        CreatedMemberId = c.String(maxLength: 128),
                        UserAgent = c.String(maxLength: 512),
                        UserHostAddress = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DataHistory");
        }
    }
}
