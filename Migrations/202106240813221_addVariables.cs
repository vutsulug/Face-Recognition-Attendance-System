namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVariables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendance", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employee", "EmployeeCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "EmployeeCode");
            DropColumn("dbo.Attendance", "IsDeleted");
        }
    }
}
