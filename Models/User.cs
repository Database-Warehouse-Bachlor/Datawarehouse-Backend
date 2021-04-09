using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Datawarehouse_Backend.Models
{
    
    public class User{

        public User()
        {

        }

        [Key]      
        public int id {get; set;}

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email {get; set;}

        [Required]        
        [DataType(DataType.Password)]
        [MinLength(4)]
        [MaxLength(255)]
        public string password {get; set;}
        [Required]

        public long tennantId { get; set; }
        public Tennant tennant { get; set; }
    }
}