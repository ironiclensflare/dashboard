namespace Dashboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Setup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        FormID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Area = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.FormID);
            
            CreateTable(
                "dbo.FormSubmissions",
                c => new
                    {
                        FormSubmissionID = c.Int(nullable: false, identity: true),
                        FormID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FormSubmissionID)
                .ForeignKey("dbo.Forms", t => t.FormID, cascadeDelete: true)
                .Index(t => t.FormID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FormSubmissions", "FormID", "dbo.Forms");
            DropIndex("dbo.FormSubmissions", new[] { "FormID" });
            DropTable("dbo.FormSubmissions");
            DropTable("dbo.Forms");
        }
    }
}
