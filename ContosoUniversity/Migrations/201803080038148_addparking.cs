namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addparking : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Parking", "Occupant_ID", "dbo.Person");
            DropIndex("dbo.Parking", new[] { "Occupant_ID" });
            AddColumn("dbo.Parking", "OccupantID", c => c.Int());
            DropColumn("dbo.Parking", "Occupant_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parking", "Occupant_ID", c => c.Int());
            DropColumn("dbo.Parking", "OccupantID");
            CreateIndex("dbo.Parking", "Occupant_ID");
            AddForeignKey("dbo.Parking", "Occupant_ID", "dbo.Person", "ID");
        }
    }
}
