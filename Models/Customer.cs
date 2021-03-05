using System;
using System.ComponentModel.DataAnnotations;

namespace Datawarehouse_Backend
{

    public class Customer {
        [Key]
        public long id { get; set;}
        public long customerId {get; set;}
        public long businessId {get; set;}
        public string customerName {get; set;}
        public string address {get; set;}
        public string zipcode {get; set;}
        public string city {get; set;}
        public Boolean isInactive {get; set;}
        
    }
}