namespace BirdsSoundsClassifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        SamplingID = c.Int(nullable: false),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longtitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Country = c.String(),
                        District = c.String(),
                    })
                .PrimaryKey(t => t.LocationID)
                .ForeignKey("dbo.Samplings", t => t.SamplingID, cascadeDelete: true)
                .Index(t => t.SamplingID);
            
            CreateTable(
                "dbo.Samplings",
                c => new
                    {
                        SamplingID = c.Int(nullable: false, identity: true),
                        BirdID = c.Int(nullable: false),
                        ObservationType = c.String(),
                        ObservationDuaration = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SamplingID)
                .ForeignKey("dbo.Species", t => t.BirdID, cascadeDelete: true)
                .Index(t => t.BirdID);
            
            CreateTable(
                "dbo.Species",
                c => new
                    {
                        BirdID = c.Int(nullable: false, identity: true),
                        CommonName = c.String(),
                    })
                .PrimaryKey(t => t.BirdID);
            
            CreateTable(
                "dbo.Taxonomies",
                c => new
                    {
                        TaxonID = c.Int(nullable: false, identity: true),
                        BirdID = c.Int(nullable: false),
                        SciName = c.String(),
                        TaxonOrder = c.String(),
                        Genus_Name = c.String(),
                        FamilyName = c.String(),
                        OrderName = c.String(),
                    })
                .PrimaryKey(t => t.TaxonID)
                .ForeignKey("dbo.Species", t => t.BirdID, cascadeDelete: true)
                .Index(t => t.BirdID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Taxonomies", "BirdID", "dbo.Species");
            DropForeignKey("dbo.Samplings", "BirdID", "dbo.Species");
            DropForeignKey("dbo.Locations", "SamplingID", "dbo.Samplings");
            DropIndex("dbo.Taxonomies", new[] { "BirdID" });
            DropIndex("dbo.Samplings", new[] { "BirdID" });
            DropIndex("dbo.Locations", new[] { "SamplingID" });
            DropTable("dbo.Taxonomies");
            DropTable("dbo.Species");
            DropTable("dbo.Samplings");
            DropTable("dbo.Locations");
        }
    }
}
