namespace BirdsSoundsClassifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingImagesFunctios : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Taxonomies", "BirdsPicture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Taxonomies", "BirdsPicture", c => c.Binary());
        }
    }
}
