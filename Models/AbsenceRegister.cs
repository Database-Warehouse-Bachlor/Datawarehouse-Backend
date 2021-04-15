using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datawarehouse_Backend.Models
{
    public class AbsenceRegister
    {
        [Key]
        public long id { get; set; }

        public long AbsenceRegisterId { get; set; }
        [Required]
        public DateTime fromDate { get; set; }
        [Required]
        public DateTime toDate { get; set; }
        [Required]
        public double duration { get; set; }
        public Boolean soleCaretaker { get; set; }
        public string abcenseType { get; set; }
        public string abcenseTypeText { get; set; }
        public string comment { get; set; }
        public string degreeDisability { get; set; }

        [ForeignKey("employee")]
        public long employeeFK { get; set; }
        public Employee employee { get; set; }
    }

}