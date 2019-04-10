using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BirdsSoundsClassifier.Models
{
    public class Taxonomy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaxonID { get; set; }
        //public Species Species { get; set; }
        //public int BirdID { get; set; }
        public string SciName { get; set; }
        public string TaxonOrder { get; set; }
        public string Genus_Name { get; set; }
        public string FamilyName { get; set; }
        public string OrderName { get; set; }
    }
}