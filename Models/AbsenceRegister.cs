using System;
using System.ComponentModel.DataAnnotations;

namespace Datawarehouse_Backend.Models
{
    public class AbsenceRegister
    {
        [Key]
        public long id { get; set; }
        [Required]
/*         public DateTime fromDate { get; set; }
        [Required]
        public DateTime toDate { get; set; }
        //int?
        [Required]
        public double duration { get; set; }
        [Required]
        public Boolean soleCaretaker { get; set; }
        [Required]
        public string abcenseType { get; set; }
        [Required]
        //public string abcenseTypeText { get; set; }
        //public string comment { get; set; }
        public string degreeDisability { get; set; } */
        
        public long employeeId { get; set; }
        public Employee employee { get; set; }

    }
}