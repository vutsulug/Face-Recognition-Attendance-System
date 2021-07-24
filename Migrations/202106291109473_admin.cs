namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "AdminID", c => c.Int());
            AddColumn("dbo.Employee", "UserName", c => c.String());
            AddColumn("dbo.Employee", "Password", c => c.String());
            AddColumn("dbo.Employee", "ConfirmPassword", c => c.String());
            AddColumn("dbo.Employee", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "Discriminator");
            DropColumn("dbo.Employee", "ConfirmPassword");
            DropColumn("dbo.Employee", "Password");
            DropColumn("dbo.Employee", "UserName");
            DropColumn("dbo.Employee", "AdminID");
        }
    }
}
