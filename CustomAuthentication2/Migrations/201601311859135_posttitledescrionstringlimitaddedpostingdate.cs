namespace CustomAuthentication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posttitledescrionstringlimitaddedpostingdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "PostingDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Posts", "Description", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Posts", "PostingDate");
        }
    }
}
