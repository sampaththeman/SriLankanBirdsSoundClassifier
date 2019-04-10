using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirdsSoundsClassifier.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class RoleAttributes
    {
        public int AttributeID { get; set; }
        public string Name { get; set; }
    }

    public class RegUserRoles
    {
        public int UserID { get; set; }
        public string AttributeName { get; set; }
    }

    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Passwords { get; set; }
    }
}