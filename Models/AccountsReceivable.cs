/* using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datawarehouse_Backend.Models
{
    public class AccountsReceivable
    {
        [Key]
        public long id { get; set; }
        public long AccountsReceivableId { get; set; }
        public DateTime recordDate { get; set; }
        public DateTime dueDate { get; set; }
        public string recordType { get; set; }
        public string customerName { get; set; }
        public decimal amount { get; set; }
        public decimal amountDue { get; set; }
        public string overdueNotice { get; set; }
        public string note { get; set; }
        public long oderId { get; set; }
        public long jobId { get; set; }

        [ForeignKey("customer")]
        public long customerFK { get; set; }
        public Customer customer { get; set; }
    }
} */