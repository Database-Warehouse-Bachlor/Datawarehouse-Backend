using System;

namespace Datawarehouse_Backend.ViewModels
{
    public class OrderView
    {
        public string jobname {get; set;}
        public decimal invoiceTotal {get; set;}
        public string clientName {get; set;}
        public DateTime endDate {get; set;}
    }
}