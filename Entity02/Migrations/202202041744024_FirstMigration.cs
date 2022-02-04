namespace Entity02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Engine", c => c.String());
            DropColumn("dbo.Cars", "Color");
            DropColumn("dbo.Cars", "Year");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "Year", c => c.Short(nullable: false));
            AddColumn("dbo.Cars", "Color", c => c.String());
            DropColumn("dbo.Cars", "Engine");
        }
    }
}
