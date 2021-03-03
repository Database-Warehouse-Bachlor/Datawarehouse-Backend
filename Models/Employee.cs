using System;
using System.ComponentModel.DataAnnotations;

namespace Datawarehouse_Backend
{

    public class Employee
    {
        [Key]
        public long id { get; set; }
        public long employeeId { get; set; }
        public long tennantId { get; set; }
        public string employeeName { get; set; }
        public DateTime birthdate { get; set; }
        public long posistionCategoryId { get; set; }
        public int employmentRate { get; set; }
        public DateTime startDate { get; set; }
        public DateTime leaveDate { get; set; }
        public string gender { get; set; }
        public string ssbPositionCode { get; set; }
        public string ssbPositionText { get; set; }
        public string ssbPayType { get; set; }
        public string status { get; set; }
        public string statusText { get; set; }
        public Boolean isCaseworker { get; set; }
        public string employmentType { get; set; }
    }

}