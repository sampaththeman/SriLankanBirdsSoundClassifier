namespace BirdsSoundsClassifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFkBirdID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Taxonomies", "BirdID", "dbo.Species");
            DropIndex("dbo.Taxonomies", new[] { "BirdID" });
            RenameColumn(table: "dbo.Taxonomies", name: "BirdID", newName: "Species_BirdID");
            AlterColumn("dbo.Taxonomies", "Species_BirdID", c => c.Int());
            CreateIndex("dbo.Taxonomies", "Species_BirdID");
            AddForeignKey("dbo.Taxonomies", "Species_BirdID", "dbo.Species", "BirdID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Taxonomies", "Species_BirdID", "dbo.Species");
            DropIndex("dbo.Taxonomies", new[] { "Species_BirdID" });
            AlterColumn("dbo.Taxonomies", "Species_BirdID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Taxonomies", name: "Species_BirdID", newName: "BirdID");
            CreateIndex("dbo.Taxonomies", "BirdID");
            AddForeignKey("dbo.Taxonomies", "BirdID", "dbo.Species", "BirdID", cascadeDelete: true);
        }
    }
}
