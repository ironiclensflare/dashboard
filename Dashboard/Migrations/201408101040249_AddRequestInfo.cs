namespace Dashboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequestInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormSubmissions", "RequestHeaders", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FormSubmissions", "RequestHeaders");
        }
    }
}
