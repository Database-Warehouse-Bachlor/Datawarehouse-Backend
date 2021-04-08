using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Datawarehouse_Backend
{

    public class Customer {
        [Key]
        public long id { get; set;}
        public long customerId {get; set;}
        public Tennant tennant {get; set;}
        public String customerName {get; set;}
        public String address {get; set;}
        public int zipcode {get; set;}
        public string city {get; set;}
        public Boolean isInactive {get; set;}
        public ICollection<InvoiceOutbound> invoicesOutbound { get; set; } = new List<InvoiceOutbound>();
        public ICollection<Accountsreceivable> accountsreceivables { get; set; } = new List<Accountsreceivable>();
        public ICollection<Order> orders { get; set; } = new List<Order>();

        
    }
}