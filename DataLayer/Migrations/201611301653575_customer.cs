namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "Customer_Id", c => c.Int());
            AddColumn("dbo.Door", "Address_Id", c => c.Int());
            AlterColumn("dbo.Address", "StreetName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Address", "StreetNumber", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.Address", "City", c => c.String(nullable: false, maxLength: 35));
            AlterColumn("dbo.Address", "PostalCode", c => c.String(nullable: false));
            CreateIndex("dbo.Address", "Customer_Id");
            CreateIndex("dbo.Door", "Address_Id");
            AddForeignKey("dbo.Door", "Address_Id", "dbo.Address", "Id");
            AddForeignKey("dbo.Address", "Customer_Id", "dbo.Customer", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Address", "Customer_Id", "dbo.Customer");
            DropForeignKey("dbo.Door", "Address_Id", "dbo.Address");
            DropIndex("dbo.Door", new[] { "Address_Id" });
            DropIndex("dbo.Address", new[] { "Customer_Id" });
            AlterColumn("dbo.Address", "PostalCode", c => c.String());
            AlterColumn("dbo.Address", "City", c => c.String());
            AlterColumn("dbo.Address", "StreetNumber", c => c.String());
            AlterColumn("dbo.Address", "StreetName", c => c.String());
            DropColumn("dbo.Door", "Address_Id");
            DropColumn("dbo.Address", "Customer_Id");
        }
    }
}
