using System.ComponentModel.DataAnnotations;

//Posteringer
namespace Datawarehouse_Backend.Models
{
    public class Post
    {
        [Key]
        public long id {get; set;}
        public long postId {get; set;}
        public string description {get; set;}
        public decimal amount {get; set;}
        public long MVAcode {get; set;}

        public Account
        public Voucher
        public Customer

    }
}