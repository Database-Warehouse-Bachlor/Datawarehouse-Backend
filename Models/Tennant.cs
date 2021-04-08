using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Datawarehouse_Backend
{
    public class Tennant {
        [key]
         public long id { get; set; }
         public string tennantName { get; set; }
         [Required]
         public string businessId { get; set; }
         [Required]
         public string apiKey { get; set; }
         public ICollection<User> users { get; set; } = new List<User>();
         public ICollection<Employee> employees { get; set; } = new List<Employee>();
         public ICollection<InvoiceInbound> invoicesInbound { get; set; } = new List<InvoiceInbound>();
         public ICollection<BalanceAndBudget> bnb { get; set; } = new List<BalanceAndBudget>();
         public ICollection<Customer> customers { get; set; } = new List<Customer>();
         public ICollection<Order> orders { get; set; } = new List<Order>();
    }
}