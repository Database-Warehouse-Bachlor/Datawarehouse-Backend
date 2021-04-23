using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 
*/

namespace Datawarehouse_Backend.Models
{
    public class Client
    {
        [Key]
        public long id { get; set; }
        public long clientId { get; set; }
        public string clientName { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public bool isInactive { get; set; }
        public bool customer {get; set;}

        [ForeignKey("tennant")]
        public long tennantFK { get; set; }
        public Tennant tennant { get; set; }

        public ICollection<Voucher> vouchers { get; set; } = new List<Voucher>();
        public ICollection<Order> orders { get; set; } = new List<Order>();

    }
}