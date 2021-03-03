using System;
using System.ComponentModel.DataAnnotations;

namespace Datawarehouse_Backend
{
    public class BalanceAndBudget {
        [Key]
        public long id { get; set;}
        public string account { get; set;}
        public long tennantId { get; set;}
        public string name { get; set;}
        public DateTime periodDate { get; set;}
        public double startBalance { get; set;}
        public double periodBalance { get; set;}
        public double endBalance { get; set;}
        public string department { get; set;}
    }
}