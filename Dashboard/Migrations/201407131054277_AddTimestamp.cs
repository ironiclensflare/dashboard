namespace Dashboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimestamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormSubmissions", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FormSubmissions", "Created");
        }
    }
}
