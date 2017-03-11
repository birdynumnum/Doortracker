namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetime21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Door", "YearOfManufacture", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Door", "YearOfManufacture", c => c.DateTime(nullable: false));
        }
    }
}
