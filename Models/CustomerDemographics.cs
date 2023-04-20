using System;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models
{
    public class CustomerDemographics
    {
        [Key]
        
        public string CustomerTypeID { get; set; }
        
        public string CustomerDesc{ get; set; }

    
    }
}
