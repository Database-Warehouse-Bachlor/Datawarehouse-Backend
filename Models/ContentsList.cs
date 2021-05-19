using System;
using System.Collections.Generic;
namespace Datawarehouse_Backend.Models
{


    //Object that is used when handling incoming data
    public class ContentsList
    {
        public string businessId { get; set; }
        public string apiKey { get; set; }
        public string tennantName { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<Client> Clients { get; set; }
        public List<BalanceAndBudget> BalanceAndBudgets { get; set; }
        public List<AbsenceRegister> AbsenceRegisters { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Order> Orders { get; set; }
        public List<TimeRegister> TimeRegisters { get; set; }
        public List<InvoiceLine> InvoiceLines {get; set;}       
        public List<Voucher> Vouchers {get; set;}       
        public List<Post> Posts {get; set;}       
        public List<Account> Accounts {get; set;}       
        public List<FinancialYear> FinancialYears {get; set;}   
    }


    //Exception for when the program cant connect a model to another with a foreign key
    [Serializable]
    class InvalidModelFK : Exception
    {
        public InvalidModelFK()
        {

        }

        public InvalidModelFK(String name)
            : base(String.Format("Invalid ModelFK", name))
        {

        }
    }
}