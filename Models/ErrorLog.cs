using System;
using System.ComponentModel.DataAnnotations;

namespace Datawarehouse_Backend.Models
{
    public class ErrorLog
    {
        [Key]
        public long id { get; set; }
        [Required]
        public string errorType { get; set; }
        [Required]
        public string errorMessage { get; set; }
        [Required]
        public DateTime timeOfError { get; set; }

    }
}