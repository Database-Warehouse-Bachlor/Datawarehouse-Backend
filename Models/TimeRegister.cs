using System;
using System.ComponentModel.DataAnnotations;

namespace Datawarehouse_Backend
{
    public class TimeRegister {
        [Key]
         public long id { get; set; }
         public Employee employee {get; set;}
         public Boolean isCaseworker { get; set; }
         public string personName { get; set; }
         public string personDepartment { get; set; }
         public string personDepartmentName { get; set; }
         public int year { get; set; }
         public DateTime recordDate { get; set; }
         public string recordDepartment { get; set; }
         public string recordDepartmentName { get; set; }
         public string payType { get; set; }
         public string payTypeName { get; set; }
         public string qyt { get; set; }
         public double rate { get; set; }
         public double amount { get; set; }
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
    }
}