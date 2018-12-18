namespace TestTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddropdownlistfordaysofweektocustomersmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "DayAvailableValue", c => c.Int(nullable: false));
            DropColumn("dbo.Customers", "DayAvailable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "DayAvailable", c => c.String());
            DropColumn("dbo.Customers", "DayAvailableValue");
        }
    }
}
