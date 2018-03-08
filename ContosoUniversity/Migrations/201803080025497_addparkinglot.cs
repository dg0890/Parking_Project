namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addparkinglot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parking",
                c => new
                    {
                        ParkingID = c.Int(nullable: false, identity: true),
                        Occupant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ParkingID)
                .ForeignKey("dbo.Person", t => t.Occupant_ID)
                .Index(t => t.Occupant_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parking", "Occupant_ID", "dbo.Person");
            DropIndex("dbo.Parking", new[] { "Occupant_ID" });
            DropTable("dbo.Parking");
        }
    }
}
