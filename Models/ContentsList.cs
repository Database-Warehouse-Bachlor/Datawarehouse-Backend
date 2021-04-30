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
        public List<Invoice> Invoice { get; set; }
        public List<Client> Client { get; set; }
        public List<BalanceAndBudget> BalanceAndBudget { get; set; }
        public List<AbsenceRegister> AbsenceRegister { get; set; }
        public List<Employee> Employee { get; set; }
        public List<Order> Order { get; set; }
        public List<TimeRegister> TimeRegister { get; set; }
        public List<InvoiceLine> InvoiceLine {get; set;}       
        public List<Voucher> Voucher {get; set;}       
        public List<Post> Post {get; set;}       
        public List<Account> Account {get; set;}       
        public List<FinancialYear> FinancialYear {get; set;}   
      //  public List<AccountsReceivable> Accountsreceivable { get; set; }
    }
}