namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PressMember",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 512),
                        LastName = c.String(nullable: false, maxLength: 512),
                        OfficePhone = c.String(nullable: false),
                        MobilePhone = c.String(nullable: false),
                        Fax = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Email2 = c.String(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Adress = c.String(nullable: false),
                        Notes = c.String(nullable: false),
                        About = c.String(nullable: false),
                        Block = c.Boolean(nullable: false),
                        FirmId = c.Int(),
                        TaskId = c.Int(),
                        DistrictId = c.Int(),
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
            
            CreateTable(
                "dbo.ContactRecord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactDate = c.DateTime(nullable: false),
                        Notes = c.String(nullable: false),
                        PressMemberId = c.Int(nullable: false),
                        ContactTypeId = c.Int(nullable: false),
                        LcvId = c.Int(nullable: false),
                        participationStatus = c.Boolean(nullable: false),
                        accidentStatus = c.Boolean(),
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
                "dbo.Edition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 512),
                        Adress = c.String(nullable: false),
                        EditionTypeId = c.Int(nullable: false),
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
                "dbo.Event",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 512),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        EventPlace = c.String(nullable: false),
                        EventAdress = c.String(nullable: false),
                        EventTypeId = c.Int(nullable: false),
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
                "dbo.Firm",
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
            
            CreateTable(
                "dbo.PickListCategory",
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
            
            CreateTable(
                "dbo.PickList",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 512),
                        CategoryId = c.Int(),
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
                "dbo.PressMemberLicense",
                c => new
                    {
                        LicenseId = c.Int(nullable: false),
                        PressMemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LicenseId, t.PressMemberId })
                .ForeignKey("dbo.PickList", t => t.LicenseId, cascadeDelete: true)
                .ForeignKey("dbo.PressMember", t => t.PressMemberId, cascadeDelete: true)
                .Index(t => t.LicenseId)
                .Index(t => t.PressMemberId);
            
            CreateTable(
                "dbo.Vehicle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Marka = c.String(nullable: false, maxLength: 512),
                        Serial = c.String(nullable: false, maxLength: 512),
                        Model = c.String(nullable: false, maxLength: 512),
                        Plate = c.String(nullable: false, maxLength: 512),
                        ModelDate = c.DateTime(nullable: false),
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
        
        public override void Down()
        {
            DropForeignKey("dbo.PressMemberLicense", "PressMemberId", "dbo.PressMember");
            DropForeignKey("dbo.PressMemberLicense", "LicenseId", "dbo.PickList");
            DropIndex("dbo.PressMemberLicense", new[] { "PressMemberId" });
            DropIndex("dbo.PressMemberLicense", new[] { "LicenseId" });
            DropTable("dbo.Vehicle");
            DropTable("dbo.PressMemberLicense");
            DropTable("dbo.PickList");
            DropTable("dbo.PickListCategory");
            DropTable("dbo.Firm");
            DropTable("dbo.Event");
            DropTable("dbo.Edition");
            DropTable("dbo.District");
            DropTable("dbo.ContactRecord");
            DropTable("dbo.City");
            DropTable("dbo.PressMember");
        }
    }
}
