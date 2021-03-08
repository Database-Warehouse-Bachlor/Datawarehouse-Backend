using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Datawarehouse_Backend.Models
{
    
    public class User{

        [Key]
        
        public int id {get; set;}

//orgnr er string i tilfelle et orgnr starter med 0..
        [Required]
        [Column(TypeName = "varchar(10)")] 
        public string orgNr {get; set;}

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email {get; set;}

        [Required]        
        [DataType(DataType.Password)]
        [MinLength(4)]
        [MaxLength(50)]
        public string password {get; set;}
    }
}