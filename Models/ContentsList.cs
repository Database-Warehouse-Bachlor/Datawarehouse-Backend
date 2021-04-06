using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Datawarehouse_Backend.Models {

    public class ContentsList
    {
        public List<InvoiceInbound> InvoiceInbound { get; set; }
        public List<InvoiceOutbound> InvoiceOutbound {get; set;}
        public List<Customer> Customer {get; set;}
        public string businessId { get; set; }
        public string tennantName { get; set; }
        public string apiKey { get; set; }
    }
}