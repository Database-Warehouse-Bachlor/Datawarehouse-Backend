using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Bilag
namespace Datawarehouse_Backend.Models
{
    public class Voucher
    {
        [Key]
        public long id { get; set; }
        public long voucherId { get; set; }
        public long number { get; set; }
        public string Type { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public int paymentId { get; set; }
        public long clientId { get; set; }
        public Invoice invoice { get; set; }

        [ForeignKey("client")]
        public long clientFK { get; set; }
        public Client client { get; set; }

        public ICollection<Post> posts { get; set; } = new List<Post>();

    }
}