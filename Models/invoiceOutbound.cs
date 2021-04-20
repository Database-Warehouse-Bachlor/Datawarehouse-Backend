using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datawarehouse_Backend.Models
{

    public class InvoiceOutbound
    {
        [Key]
        public long id { get; set; }
        public long invoiceOutboundId { get; set; }
        public DateTime invoiceDue { get; set; }
        public long jobId { get; set; }
        public DateTime invoiceDate { get; set; }
        public decimal invoiceExVat { get; set; }
        public decimal invoiceIncVat { get; set; }
        public decimal amountExVat { get; set; }
        public decimal amountIncVat { get; set; }
        public decimal amountTotal { get; set; }

        [ForeignKey("order")]
        public long orderFK { get; set; }
        public virtual Order order { get; set; }

        [ForeignKey("customer")]
        public long customerFK { get; set; }
        public Customer customer { get; set; }
    }
}