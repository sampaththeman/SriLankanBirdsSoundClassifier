namespace BirdsSoundsClassifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class httposted : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Taxonomies", "Imagepath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Taxonomies", "Imagepath", c => c.String());
        }
    }
}
