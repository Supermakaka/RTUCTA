namespace BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Car",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarManufacurer = c.String(),
                        CarModel = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Time = c.DateTime(nullable: false),
                        Altitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Accuracy = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Car", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Location", "CarId", "dbo.Car");
            DropIndex("dbo.Location", new[] { "CarId" });
            DropTable("dbo.Location");
            DropTable("dbo.Car");
        }
    }
}
