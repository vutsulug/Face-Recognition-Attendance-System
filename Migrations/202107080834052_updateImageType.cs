namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateImageType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employee", "EmployeePhoto", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employee", "EmployeePhoto", c => c.Binary());
        }
    }
}
