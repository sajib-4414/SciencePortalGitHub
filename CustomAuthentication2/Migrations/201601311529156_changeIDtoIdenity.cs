namespace CustomAuthentication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeIDtoIdenity : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Posts");
            AlterColumn("dbo.Posts", "ID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Posts", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Posts");
            AlterColumn("dbo.Posts", "ID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Posts", "ID");
        }
    }
}
