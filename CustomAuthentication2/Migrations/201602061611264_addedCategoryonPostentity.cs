namespace CustomAuthentication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCategoryonPostentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Category", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Category");
        }
    }
}
