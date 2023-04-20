using System;
using System.ComponentModel.DataAnnotations;


namespace NorthwindAPI.Models
{
    public class CustomerCustomerDemo
    {
        [Key]
       
        public string CustomerID{ get; set; }

        [Key]
       
        public string CustomerTypeID{ get; set; }
    }
}
