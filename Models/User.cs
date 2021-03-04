using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Datawarehouse_Backend.Models
{
    
    public class User : IdentityUser<Guid>{

        [Key]
        public int id {get; set;}

        [Required]
        public string orgNr {get; set;}

       // [DataType(DataType.EmailAddress)]
        //public string Email {get; set;}

        [DataType(DataType.Password)]
        public string password {get; set;}
    }
}