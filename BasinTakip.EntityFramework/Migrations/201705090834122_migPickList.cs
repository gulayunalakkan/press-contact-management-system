namespace BasinTakip.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migPickList : DbMigration
    {
        public override void Up()
        {
            Sql(@"
IF COL_LENGTH('dbo.PickList', 'PicListId') IS NOT NULL
    ALTER TABLE dbo.PickList DROP COLUMN PicListId;
");
        }

        public override void Down()
        {
            AddColumn("dbo.PickList", "PicListId", c => c.Int(nullable: false));
        }
    }
}
