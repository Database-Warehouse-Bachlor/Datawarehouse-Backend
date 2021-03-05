using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Datawarehouse_Backend
{
    public class Tennant {
        [key]
         public long id { get; set; }
         public string tennantName { get; set; }
         public string address { get; set; }
         public string zipcode { get; set; }
         public string city { get; set; }

         public long businessId { get; set; }
         
         public List<Employee> employee { get; set; }
         
    }
}