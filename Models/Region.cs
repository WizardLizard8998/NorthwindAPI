using System;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models
{
    public class Region
    {
        [Key]
        
        public int RegionID { get; set; }

        
        public string RegionDescription { get; set; }
    }
}
