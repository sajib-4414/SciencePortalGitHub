namespace CustomAuthentication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignkeytoComment : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "Username", newName: "Account_Username");
            RenameIndex(table: "dbo.Comments", name: "IX_Username", newName: "IX_Account_Username");
            AddColumn("dbo.Comments", "Fullname", c => c.String());
            AlterColumn("dbo.Comments", "CommentDescription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "CommentDescription", c => c.String());
            DropColumn("dbo.Comments", "Fullname");
            RenameIndex(table: "dbo.Comments", name: "IX_Account_Username", newName: "IX_Username");
            RenameColumn(table: "dbo.Comments", name: "Account_Username", newName: "Username");
        }
    }
}
