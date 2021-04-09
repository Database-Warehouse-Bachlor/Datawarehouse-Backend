using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datawarehouse_Backend.Models
{

    public class Customer {
        [Key]
        public long id { get; set;}
 /*        public String customerName {get; set;}
        public String address {get; set;}
        public int zipcode {get; set;}
        public string city {get; set;}
        public Boolean isInactive {get; set;} */

        public long tennantId { get; set; }
        public Tennant tennant {get; set;}

        public ICollection<InvoiceOutbound> invoicesOutbound { get; set; } = new List<InvoiceOutbound>();
        public ICollection<AccountsReceivable> accountsreceivables { get; set; } = new List<AccountsReceivable>();
        public ICollection<Order> orders { get; set; } = new List<Order>();

        
    }
}