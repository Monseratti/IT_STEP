namespace Olympiad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Olympaids",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Short(nullable: false),
                        IsWinter = c.Boolean(nullable: false),
                        ParentCountryId = c.Int(),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.ParentCountryId)
                .Index(t => t.ParentCountryId);
            
            CreateTable(
                "dbo.OlympaidSports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OlympaidId = c.Int(nullable: false),
                        SportsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Olympaids", t => t.OlympaidId, cascadeDelete: true)
                .ForeignKey("dbo.Sports", t => t.SportsId, cascadeDelete: true)
                .Index(t => t.OlympaidId)
                .Index(t => t.SportsId);
            
            CreateTable(
                "dbo.OlympaidResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OlSpId = c.Int(nullable: false),
                        SportsmenId = c.Int(nullable: false),
                        GoldenMedal = c.Int(nullable: false),
                        SilverMedal = c.Int(nullable: false),
                        BronzeMedal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OlympaidSports", t => t.OlSpId, cascadeDelete: true)
                .ForeignKey("dbo.Sportsmen", t => t.SportsmenId, cascadeDelete: true)
                .Index(t => t.OlSpId)
                .Index(t => t.SportsmenId);
            
            CreateTable(
                "dbo.Sportsmen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SportsmenName = c.String(),
                        BirthDay = c.DateTime(nullable: false, storeType: "date"),
                        Photo = c.Binary(maxLength: 8000),
                        CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Sports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SportsName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OlympaidSports", "SportsId", "dbo.Sports");
            DropForeignKey("dbo.OlympaidResults", "SportsmenId", "dbo.Sportsmen");
            DropForeignKey("dbo.Sportsmen", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.OlympaidResults", "OlSpId", "dbo.OlympaidSports");
            DropForeignKey("dbo.OlympaidSports", "OlympaidId", "dbo.Olympaids");
            DropForeignKey("dbo.Olympaids", "ParentCountryId", "dbo.Countries");
            DropIndex("dbo.Sportsmen", new[] { "CountryId" });
            DropIndex("dbo.OlympaidResults", new[] { "SportsmenId" });
            DropIndex("dbo.OlympaidResults", new[] { "OlSpId" });
            DropIndex("dbo.OlympaidSports", new[] { "SportsId" });
            DropIndex("dbo.OlympaidSports", new[] { "OlympaidId" });
            DropIndex("dbo.Olympaids", new[] { "ParentCountryId" });
            DropTable("dbo.Sports");
            DropTable("dbo.Sportsmen");
            DropTable("dbo.OlympaidResults");
            DropTable("dbo.OlympaidSports");
            DropTable("dbo.Olympaids");
            DropTable("dbo.Countries");
        }
    }
}
