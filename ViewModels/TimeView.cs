using System;

namespace Datawarehouse_Backend.ViewModels
{
    public class TimeView
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public string weekDay { get; set; }
        public int totalAmount { get; set; }
    }
}