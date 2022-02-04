namespace Lab14.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "isDebtor", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "isDebtor");
        }
    }
}
