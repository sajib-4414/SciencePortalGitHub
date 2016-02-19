namespace CustomAuthentication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedrequiredonPostClassUserName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Username", "dbo.Accounts");
            DropIndex("dbo.Posts", new[] { "Username" });
            AlterColumn("dbo.Posts", "Username", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Posts", "Username");
            AddForeignKey("dbo.Posts", "Username", "dbo.Accounts", "Username", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Username", "dbo.Accounts");
            DropIndex("dbo.Posts", new[] { "Username" });
            AlterColumn("dbo.Posts", "Username", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "Username");
            AddForeignKey("dbo.Posts", "Username", "dbo.Accounts", "Username");
        }
    }
}
