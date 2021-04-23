using System;

namespace Datawarehouse_Backend.ViewModels
{
    public class CustomerView
    {
        public string customerName { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public decimal amountDue {get; set;}
    }
}