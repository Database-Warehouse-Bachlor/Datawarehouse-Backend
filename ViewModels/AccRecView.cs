using System;

namespace Datawarehouse_Backend.ViewModels
{
    public class AccRecView
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public decimal amount { get; set; }
        public int daysDue { get; set; }

    }
}