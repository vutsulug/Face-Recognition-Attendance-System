namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "EmployeePhoto", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "EmployeePhoto");
        }
    }
}
