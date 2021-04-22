using System;
using System.ComponentModel.DataAnnotations;

//Bilag
namespace Datawarehouse_Backend.Models
{
    public class Voucher
    {
        [Key]
        public long id { get; set; }
        public long voucherId { get; set; }
        public long number {get; set;}
        public string Type {get; set;} 
        public string description {get; set;}
        public DateTime date {get; set;}
    }
}