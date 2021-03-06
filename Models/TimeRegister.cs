using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datawarehouse_Backend.Models
{
    public class TimeRegister
    {
        [Key]
        public long id { get; set; }
        public long timeRegisterId { get; set; }
        public DateTime recordDate { get; set; }
        public Boolean isCaseworker { get; set; }
        public string personName { get; set; }
        public string personDepartment { get; set; }
        public string personDepartmentName { get; set; }
        public int year { get; set; }
        public string recordDepartment { get; set; }
        public string recordDepartmentName { get; set; }
        public string payType { get; set; }
        public string payTypeName { get; set; }
        public int qty { get; set; }
        public double rate { get; set; }
        public decimal amount { get; set; }
        public string invoiceRate { get; set; }
        public long orderId { get; set; }
        public string workplace { get; set; }
        public string account { get; set; }
        public string workComment { get; set; }
        public string recordType { get; set; }
        public string recordTypeName { get; set; }
        public string processingCode { get; set; }
        public string viaType { get; set; }
        public string summaryType { get; set; }
        public long employeeId { get; set; }

        [ForeignKey("employee")]
        public long employeeFK { get; set; }
        public Employee employee { get; set; }
    }
}