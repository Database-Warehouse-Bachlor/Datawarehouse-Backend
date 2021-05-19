using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datawarehouse_Backend.Models
{

    public class Invoice
    {
        [Key, ForeignKey("voucher")]
        public long voucherFK { get; set; }
        public long invoiceId { get; set; }
        public DateTime dueDate { get; set; }
        public long clientId { get; set; }
        public decimal amountTotal { get; set; }
        public string specification { get; set; }
        public string invoicePdf { get; set; }
        public long orderId { get; set; }
        public long voucherId { get; set; }

        public Voucher voucher { get; set; }
        public ICollection<InvoiceLine> invoiceLines { get; set; } = new List<InvoiceLine>();
    }

}