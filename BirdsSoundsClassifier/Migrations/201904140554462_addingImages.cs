namespace BirdsSoundsClassifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Taxonomies", "Imagepath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Taxonomies", "Imagepath");
        }
    }
}
