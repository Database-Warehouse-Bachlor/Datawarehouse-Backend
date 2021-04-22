using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//Regnskapsår
namespace Datawarehouse_Backend.Models
{
    public class FinancialYear
    {
        [Key]
        public long id {get; set;}
        public long financialYearId {get; set;}
        public int year {get; set;}
        public ICollection<Account> accounts { get; set; } = new List<Account>();

    }
}