using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BirdsSoundsClassifier.Models
{

    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }


    public class BirdsContext : IdentityDbContext<ApplicationUser>
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