using System;

namespace Datawarehouse_Backend.ViewModels
{
    public class DateStatusView
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public decimal thirtyAmount { get; set; }
        public decimal sixtyAmount { get; set; }
        public decimal ninetyAmount { get; set; }
        public decimal ninetyPlusAmount { get; set; }

    }
}