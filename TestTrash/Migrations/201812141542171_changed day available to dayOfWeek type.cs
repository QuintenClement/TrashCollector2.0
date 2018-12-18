namespace TestTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeddayavailabletodayOfWeektype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "DayAvailable", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "DayAvailable");
        }
    }
}
