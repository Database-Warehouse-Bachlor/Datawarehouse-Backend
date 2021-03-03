using System.ComponentModel.DataAnnotations;

namespace Datawarehouse_Backend.Models
{
    
    public class User {

        [Key]
        [MaxLength(9)]
        [MinLength(9)]
        public int Id {get; set;}

        [Required]
        public string orgNr {get; set;}

        [DataType(DataType.EmailAddress)]
        public string Email {get; set;}

        [DataType(DataType.Password)]
        public string password {get; set;}
    }
}