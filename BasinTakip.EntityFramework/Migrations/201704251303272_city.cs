namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class city : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.City");
            DropTable("dbo.District");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.District",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 512),
                        CityId = c.Int(nullable: false),
                        Permalink = c.String(nullable: false, maxLength: 512),
                        Order = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        DeletedAt = c.DateTime(),
                        CreatedMemberId = c.String(nullable: false, maxLength: 128),
                        ModifiedMemberId = c.String(nullable: false, maxLength: 128),
                        DeletedMemberId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 512),
                        Permalink = c.String(nullable: false, maxLength: 512),
                        Order = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        DeletedAt = c.DateTime(),
                        CreatedMemberId = c.String(nullable: false, maxLength: 128),
                        ModifiedMemberId = c.String(nullable: false, maxLength: 128),
                        DeletedMemberId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
