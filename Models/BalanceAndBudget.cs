using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datawarehouse_Backend.Models
{
    public class BalanceAndBudget
    {
        [Key]
        public long id { get; set; }
        public long BalanceAndBudgetId { get; set; }
        public string name { get; set; }
        public string account { get; set; }
        public DateTime periodDate { get; set; }
        public decimal startBalance { get; set; }
        public decimal periodBalance { get; set; }
        public decimal endBalance { get; set; }
        public string department { get; set; }

        [ForeignKey("tennant")]
        public long tennantFK { get; set; }
        public Tennant tennant { get; set; }
    }
}