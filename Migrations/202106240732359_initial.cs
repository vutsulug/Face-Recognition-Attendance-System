namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendance",
                c => new
                    {
                        AttendanceID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        CheckInDateTime = c.DateTime(nullable: false),
                        CheckOutDateTime = c.DateTime(nullable: false),
                        DisplayMessage = c.String(),
                    })
                .PrimaryKey(t => t.AttendanceID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(),
                        FirstName = c.String(),
                        Surname = c.String(),
                        CellNumber = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendance", "EmployeeID", "dbo.Employee");
            DropIndex("dbo.Attendance", new[] { "EmployeeID" });
            DropTable("dbo.Employee");
            DropTable("dbo.Attendance");
        }
    }
}
