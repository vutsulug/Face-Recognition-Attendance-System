namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamCapture : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageStore",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageBase64String = c.String(),
                        CreateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ImageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImageStore");
        }
    }
}
