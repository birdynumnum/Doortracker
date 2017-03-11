namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoorAdddress : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Door", new[] { "Address_Id" });
            CreateIndex("dbo.Door", "address_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Door", new[] { "address_Id" });
            CreateIndex("dbo.Door", "Address_Id");
        }
    }
}
