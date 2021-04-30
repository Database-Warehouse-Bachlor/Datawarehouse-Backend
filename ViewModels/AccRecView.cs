using System;

namespace Datawarehouse_Backend.ViewModels
{
    public class AccRecView
    {
        public int PID { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime payDate { get; set; }
        public decimal amount { get; set; }
        public int daysDue { get; set; }

    }
}