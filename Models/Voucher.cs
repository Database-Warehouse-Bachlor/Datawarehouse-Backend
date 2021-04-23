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

        [ForeignKey("client")]
        public long clientFK { get; set; }
        public Client client { get; set; }

        [ForeignKey("invoices")]
        public long invoiceFK { get; set; }
        public virtual Invoice invoices { get; set; }
        public ICollection<Post> posts { get; set; } = new List<Post>();

    }
}