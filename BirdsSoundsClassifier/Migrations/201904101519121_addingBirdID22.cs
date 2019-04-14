namespace BirdsSoundsClassifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingBirdID22 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Taxonomies", name: "Species_BirdID", newName: "BirdID");
            RenameIndex(table: "dbo.Taxonomies", name: "IX_Species_BirdID", newName: "IX_BirdID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Taxonomies", name: "IX_BirdID", newName: "IX_Species_BirdID");
            RenameColumn(table: "dbo.Taxonomies", name: "BirdID", newName: "Species_BirdID");
        }
    }
}
