using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Datawarehouse_Backend.Models
{
    public class Tennant
    {
        [Key]
        public long id { get; set; }
        [MaxLength(50)]
        public string tennantName { get; set; }
        [MaxLength(50)]
        public string businessId { get; set; }
        [Required]
        [MaxLength(50)]
        public string apiKey { get; set; }

        public ICollection<User> users { get; set; } = new List<User>();
        public ICollection<Employee> employees { get; set; } = new List<Employee>();
        public ICollection<BalanceAndBudget> bnb { get; set; } = new List<BalanceAndBudget>();
        public ICollection<Client> clients { get; set; } = new List<Client>();
        public ICollection<Order> orders { get; set; } = new List<Order>();
        public ICollection<FinancialYear> financialYears { get; set; } = new List<FinancialYear>();
    }
}