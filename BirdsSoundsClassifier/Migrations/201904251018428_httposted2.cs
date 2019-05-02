namespace BirdsSoundsClassifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class httposted2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Taxonomies", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Taxonomies", "ImageURL");
        }
    }
}
