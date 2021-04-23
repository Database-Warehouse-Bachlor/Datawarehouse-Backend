using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datawarehouse_Backend.Models
{

    public class Invoice
    {
        [Key]
        public long id { get; set; }
        public long invoiceId { get; set; }
        public string clientId { get; set; }
        public DateTime invoiceDate { get; set; }
        public decimal amountTotal { get; set; }
        public string specification { get; set; }
        public string invoicePdf { get; set; }
        public long orderId { get; set; }

        [ForeignKey("voucher")]
        public long vouchertFK { get; set; }
        public virtual Voucher voucher { get; set; }

        public ICollection<InvoiceLine> invoiceLines { get; set; } = new List<InvoiceLine>();

    }

}