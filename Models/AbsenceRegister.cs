using System;
using System.ComponentModel.DataAnnotations;

namespace Datawarehouse_Backend
{
    public class AbsenceRegister
    {
        [Key]
        public long id { get; set;}
        public long absenceId { get; set;}
        [Required]
        public Employee employee {get; set;}
        [Required]
        public DateTime fromDate { get; set; }
        
        public DateTime toDate { get; set; }
       
        //int?
        public double duration { get; set; }

        public Boolean soleCaretaker { get; set; }

        public string abcenseType { get; set; }

        //public string abcenseTypeText { get; set; }
        //public string comment { get; set; }

        public string degreeDisability { get; set; }

    }
}