using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datawarehouse_Backend.Models
{
    public class Order
    {
        [Key]
        public long id { get; set; }
        public long orderId { get; set; }
        public string orderType { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime plannedDelivery { get; set; }
        public DateTime startedDelivery { get; set; }
        public DateTime endDate { get; set; }
        public DateTime confimedDate { get; set; }
        public DateTime lastChanged { get; set; }
        public DateTime warrantyDate { get; set; }
        public string caseHandler { get; set; }
        public string clientName { get; set; }
        public long jobId { get; set; }
        public string jobName { get; set; }
        public long jobSiteId { get; set; }
        public Boolean isFixedPrice { get; set; }
        public double fixedPriceAmount { get; set; }
        public string materials { get; set; }
        public double hoursOfWork { get; set; }
        public Boolean hasWarranty { get; set; }
        public string description { get; set; }
        public long clientId { get; set; }

        [ForeignKey("tennant")]
        public long tennantFK { get; set; }
        public Tennant tennant { get; set; }

        [ForeignKey("client")]
        public long clientFK { get; set; }
        public Client client { get; set; }

    }
}