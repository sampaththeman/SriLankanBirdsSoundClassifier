namespace BirdsSoundsClassifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AudioUploadFeature : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Samplings", "BirdID", "dbo.Species");
            DropIndex("dbo.Samplings", new[] { "BirdID" });
            RenameColumn(table: "dbo.Samplings", name: "BirdID", newName: "Species_BirdID");
            AddColumn("dbo.Samplings", "AudioFileName", c => c.String());
            AlterColumn("dbo.Samplings", "Species_BirdID", c => c.Int());
            CreateIndex("dbo.Samplings", "Species_BirdID");
            AddForeignKey("dbo.Samplings", "Species_BirdID", "dbo.Species", "BirdID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Samplings", "Species_BirdID", "dbo.Species");
            DropIndex("dbo.Samplings", new[] { "Species_BirdID" });
            AlterColumn("dbo.Samplings", "Species_BirdID", c => c.Int(nullable: false));
            DropColumn("dbo.Samplings", "AudioFileName");
            RenameColumn(table: "dbo.Samplings", name: "Species_BirdID", newName: "BirdID");
            CreateIndex("dbo.Samplings", "BirdID");
            AddForeignKey("dbo.Samplings", "BirdID", "dbo.Species", "BirdID", cascadeDelete: true);
        }
    }
}
