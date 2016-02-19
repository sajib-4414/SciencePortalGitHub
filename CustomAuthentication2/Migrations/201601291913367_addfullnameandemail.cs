namespace CustomAuthentication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfullnameandemail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Email", c => c.String());
            AddColumn("dbo.Accounts", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "FullName");
            DropColumn("dbo.Accounts", "Email");
        }
    }
}
