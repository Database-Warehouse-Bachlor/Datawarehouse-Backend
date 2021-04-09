using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Datawarehouse_Backend.Models {

    public class InvoiceInbound {
        [Key]
        public long id { get; set; }
 /*        public long jobId { get; set; }
        public long supplierId { get; set; }
        public long wholesalerId { get; set; }
        public DateTime invoiceDate { get; set; }
        public double amountTotal { get; set; }
        public string specification { get; set; }
        public string invoicePdf { get; set; } */
        
        public long tennantId { get; set; }
        public Tennant tennant { get; set; }
        
    }

}