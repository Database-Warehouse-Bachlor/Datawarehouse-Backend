using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Datawarehouse_Backend.Models {


    //Object that is used when handling incoming data
    public class ContentsList
    {
        public string OrgNummer { get; set; }
        public string API_key { get; set; }
        public Tennant tennant{get; set;}
        public List<InvoiceInbound> InvoiceInbound { get; set; }
        public List<InvoiceOutbound> InvoiceOutbound { get; set;}
        public List<Customer> Customer { get; set; }
        public List<BalanceAndBudget> BalanceAndBudget { get; set; }
        public List<AbsenceRegister> AbsenceRegister { get; set; } 
        public List<Accountsreceivable> Accountsreceivable{ get; set; }
        public List<Employee> Employee { get; set; }
        public List<Order> Order { get; set; }
        public List<TimeRegister> TimeRegister { get; set; }
    }
}