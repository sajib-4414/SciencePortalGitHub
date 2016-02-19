namespace CustomAuthentication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimageinPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "image");
        }
    }
}
