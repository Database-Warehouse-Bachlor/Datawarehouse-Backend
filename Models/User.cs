using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datawarehouse_Backend.Models
{

    public class User
    {

        [Key]
        public int id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(4)]
        [MaxLength(255)]
        public string password { get; set; }

        public string role { get; set; }

        [ForeignKey("tennant")]
        public long tennantFK { get; set; }
        public Tennant tennant { get; set; }
    }
}