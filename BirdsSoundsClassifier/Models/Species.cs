using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BirdsSoundsClassifier.Models
{
    public class Species
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BirdID { get; set; }
        public string CommonName { get; set; }

        public ICollection<Taxonomy> Taxonomies { get; set; }
        public ICollection<Sampling> Sampling { get; set; }
        
    }
}