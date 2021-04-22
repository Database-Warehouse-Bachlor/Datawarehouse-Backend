using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datawarehouse_Backend.Models
{

    public class InvoiceLine
    {
        [Key]
        public long id { get; set; }
        public long invoiceLineId { get; set; }
        public string productName { get; set; }
        public int quantity { get; set; }
        public string unit { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public decimal amountTotal { get; set; }
        public decimal discount { get; set; }
        
        [ForeignKey("invoice")]
        public long invoiceFK { get; set; }
        public Invoice invoice { get; set; }

    }

}