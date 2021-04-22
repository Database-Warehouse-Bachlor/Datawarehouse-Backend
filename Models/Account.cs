using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Konto
namespace Datawarehouse_Backend.Models
{
    public class Account
    {

        [Key]
        public long id { get; set; }
        public long accountId { get; set; }
        public int number { get; set; }
        public string description { get; set; }
        public string type { get; set; } //Gjeld / Egenkapital / Resultat...
        public long MVAcode { get; set; }
        public long SAFTcode { get; set; }

        [ForeignKey("FinancialYear")]
        public long financialFK {get; set;}
        public FinancialYear FinancialYear {get; set;}

        public ICollection<Post> posts { get; set; } = new List<Post>();
    }
}
