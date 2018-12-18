namespace TestTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeddayavailabletostringtype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "DayAvailable", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "DayAvailable", c => c.Int(nullable: false));
        }
    }
}
