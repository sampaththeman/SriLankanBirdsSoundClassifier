using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BirdsSoundsClassifier.Models
{
    public class BirdsContext : DbContext
    {
      
        public BirdsContext() : base("BirdsContext")
            {
                //Turn off the Initializer
                Database.SetInitializer<BirdsContext>(null);
            }
        public static BirdsContext Create()
            {

                return new BirdsContext();
            }

        public DbSet<Species> Species { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Taxonomy> Taxonomies { get; set; }
        public DbSet<Sampling> Samplings { get; set; }



    }
}