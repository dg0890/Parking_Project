namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databasegenteratednone : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Parking");
            AlterColumn("dbo.Parking", "ParkingID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Parking", "ParkingID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Parking");
            AlterColumn("dbo.Parking", "ParkingID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Parking", "ParkingID");
        }
    }
}
