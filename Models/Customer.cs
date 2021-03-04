using System;
using System.ComponentModel.DataAnnotations;

namespace Datawarehouse_Backend.Models
{

    public class Customer {
        [Key]
        public long id { get; set;}
        public long customerId {get; set;}
        public long businessId {get; set;}
        public String customerName {get; set;}
        public String address {get; set;}
        public int zipcode {get; set;}
        public string city {get; set;}
        public Boolean isInactive {get; set;}
        
    }
}