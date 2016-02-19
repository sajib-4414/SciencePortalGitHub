namespace CustomAuthentication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrequiredonemailandfullname : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Accounts", "FullName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "FullName", c => c.String());
            AlterColumn("dbo.Accounts", "Email", c => c.String());
        }
    }
}
