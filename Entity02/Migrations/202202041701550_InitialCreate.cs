namespace Entity02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(),
                        Color = c.String(),
                        Year = c.Short(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MarkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Marks", t => t.MarkId, cascadeDelete: true)
                .Index(t => t.MarkId);
            
            CreateTable(
                "dbo.Marks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MarkName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateSale = c.DateTime(nullable: false),
                        CarId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        WorkerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Workers", t => t.WorkerId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.CustomerId)
                .Index(t => t.WorkerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CountryMarks",
                c => new
                    {
                        Country_ID = c.Int(nullable: false),
                        Mark_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Country_ID, t.Mark_ID })
                .ForeignKey("dbo.Countries", t => t.Country_ID, cascadeDelete: true)
                .ForeignKey("dbo.Marks", t => t.Mark_ID, cascadeDelete: true)
                .Index(t => t.Country_ID)
                .Index(t => t.Mark_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Sales", "CarId", "dbo.Cars");
            DropForeignKey("dbo.CountryMarks", "Mark_ID", "dbo.Marks");
            DropForeignKey("dbo.CountryMarks", "Country_ID", "dbo.Countries");
            DropForeignKey("dbo.Cars", "MarkId", "dbo.Marks");
            DropIndex("dbo.CountryMarks", new[] { "Mark_ID" });
            DropIndex("dbo.CountryMarks", new[] { "Country_ID" });
            DropIndex("dbo.Sales", new[] { "WorkerId" });
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.Sales", new[] { "CarId" });
            DropIndex("dbo.Cars", new[] { "MarkId" });
            DropTable("dbo.CountryMarks");
            DropTable("dbo.Workers");
            DropTable("dbo.Customers");
            DropTable("dbo.Sales");
            DropTable("dbo.Countries");
            DropTable("dbo.Marks");
            DropTable("dbo.Cars");
        }
    }
}
