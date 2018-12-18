namespace TestTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedlongitudeandlatitudetocustomersmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Customers", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Longitude");
            DropColumn("dbo.Customers", "Latitude");
        }
    }
}
