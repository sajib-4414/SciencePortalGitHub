namespace CustomAuthentication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedrequiredOnCategory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Category", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Category", c => c.String());
        }
    }
}
