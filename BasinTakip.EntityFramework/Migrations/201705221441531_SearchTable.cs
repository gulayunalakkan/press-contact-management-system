namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SearchTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SearchTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityId = c.String(maxLength: 128),
                        EntityType = c.String(maxLength: 128),
                        SearchData = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SearchTable");
        }
    }
}
