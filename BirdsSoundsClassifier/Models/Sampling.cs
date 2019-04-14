using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BirdsSoundsClassifier.Models
{
    public class Sampling
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SamplingID { get; set; }
        //public Species Species { get; set; }
        //public int BirdID { get; set; }
        public string AudioFileName { get; set; }
        public string ObservationType { get; set; }
        public string ObservationDuaration { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }
        public ICollection<Location> Locations { get; set; }
    }

    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationID { get; set; }
        public Sampling Sampling { get; set; }
        public int SamplingID { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longtitude { get; set; }
        public string Country { get; set; }
        public string District { get; set; }
    }
}