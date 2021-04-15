using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datawarehouse_Backend.Models {

    public class InvoiceOutbound {
        [Key]
        public long id { get; set; }
        public DateTime invoiceDue { get; set; }
        public long jobId { get; set; }
        public DateTime invoiceDate { get; set; }
        public double invoiceExVat { get; set; }
        public double invoiceIncVat { get; set; }
        public double amountExVat { get; set; }
        public double amountIncVat { get; set; }
        public double amountTotal { get; set; } 

        [ForeignKey("order")]
        public long orderId { get; set; }
        public virtual Order order { get; set; }

        public long customerId { get; set; }
        public Customer customer { get; set; }
    }
}