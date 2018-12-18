namespace TestTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedmapmodel : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Maps");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Maps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InputAddress = c.String(),
                        Latitude = c.Int(nullable: false),
                        Longitude = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
