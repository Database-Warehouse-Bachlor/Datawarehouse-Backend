using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Datawarehouse_Backend.Models
{


    //Object that is used when handling incoming data
    public class ContentsList
    {
        public string businessId { get; set; }
        public string apiKey { get; set; }
        public string tennantName { get; set; }
        public List<Invoice> InvoiceInbound { get; set; }
        public List<Invoice> InvoiceOutbound { get; set; }
        public List<Customer> Customer { get; set; }
        public List<BalanceAndBudget> BalanceAndBudget { get; set; }
        public List<AbsenceRegister> AbsenceRegister { get; set; }
        public List<AccountsReceivable> Accountsreceivable { get; set; }
        public List<Employee> Employee { get; set; }
        public List<Order> Order { get; set; }
        public List<TimeRegister> TimeRegister { get; set; }
    }
}