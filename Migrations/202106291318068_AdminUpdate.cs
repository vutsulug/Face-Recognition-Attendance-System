namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminUpdate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employee", "AdminID");
            DropColumn("dbo.Employee", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employee", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Employee", "AdminID", c => c.Int());
        }
    }
}
