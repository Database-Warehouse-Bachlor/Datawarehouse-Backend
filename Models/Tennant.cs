using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Datawarehouse_Backend.Models
{
    public class Tennant {
        [Key]
         public long id { get; set; }
         public string tennantName { get; set; }
         [Required]
         public string businessId { get; set; }
         [Required]
         public string apiKey { get; set; }
         public ICollection<User> users { get; set; } = new List<User>();
    }
}