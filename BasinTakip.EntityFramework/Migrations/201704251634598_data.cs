namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class data : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Edition", "EditionTypeName", c => c.String());
            AddColumn("dbo.Edition", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Edition", "Discriminator");
            DropColumn("dbo.Edition", "EditionTypeName");
        }
    }
}
