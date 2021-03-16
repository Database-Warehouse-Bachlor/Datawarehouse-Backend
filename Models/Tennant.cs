using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Datawarehouse_Backend.Models
{
    public class Tennant {
        [Key]
         public long id { get; set; }
         public string tennantName { get; set; }
         public string address { get; set; }
         public int zipcode { get; set; }
         public string city { get; set; }
         public string businessId { get; set; }
         public ICollection<User> users { get; set; } = new List<User>();
    }
}