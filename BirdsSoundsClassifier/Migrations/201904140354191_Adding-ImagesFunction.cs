namespace BirdsSoundsClassifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingImagesFunction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Taxonomies", "BirdsPicture", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Taxonomies", "BirdsPicture");
        }
    }
}
