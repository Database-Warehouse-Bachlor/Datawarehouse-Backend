using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Posteringer
namespace Datawarehouse_Backend.Models
{
    public class Post
    {
        [Key]
        public long id { get; set; }
        public long postId { get; set; }
        public string description { get; set; }
        public decimal amount { get; set; }
        public long MVAcode { get; set; }
        public long accountId { get; set; }
        public long voucherId { get; set; }

        [ForeignKey("voucher")]
        public long voucherFK { get; set; }
        public Voucher voucher { get; set; }
        
        [ForeignKey("account")]
        public long accountFK { get; set; }
        public Account account { get; set; }

    }
}