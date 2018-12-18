namespace TestTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedDayAvailableValuefromcustomersmodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "DayAvailableValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "DayAvailableValue", c => c.Int(nullable: false));
        }
    }
}
