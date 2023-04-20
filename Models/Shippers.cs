using System;
using System.ComponentModel.DataAnnotations;


namespace NorthwindAPI.Models
{
    public class Shippers
    {
        [Key]
        
        public int ShipperId { get; set; }

        public string CompanyName { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
