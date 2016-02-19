namespace CustomAuthentication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeroleasString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Roles", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "Roles");
        }
    }
}
