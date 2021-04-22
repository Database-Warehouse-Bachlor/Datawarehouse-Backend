using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Regnskapsår
namespace Datawarehouse_Backend.Models
{
    public class FinancialYear
    {
        [Key]
        public long id {get; set;}
        public long financialYearId {get; set;}
        public int year {get; set;}
        public int customerAccount {get; set;}
        public int supplierAccount {get; set;}
        public ICollection<Account> accounts { get; set; } = new List<Account>();
        
        [ForeignKey("tennant")]
        public long tennantFK { get; set; }
        public Tennant tennant { get; set; }

    }
}