using System;
using System.ComponentModel.DataAnnotations;

namespace Datawarehouse_Backend.Models
{
    public class AccountsReceivable
    {
        [Key]
        public long id { get; set;}
        /* 
        public DateTime recordDate { get; set;}
        public DateTime dueDate { get; set;}
        public string recordType { get; set;}
        public string customerName { get; set;}
        public double amount { get; set;}
        public double amountDue { get; set;}
        public string overdueNotice { get; set;}
        public string note { get; set;}
        public long oderId { get; set;}
        public long jobId { get; set;} */
        
        public long customerId {get; set;}
        public Customer customer { get; set;}
    }
}