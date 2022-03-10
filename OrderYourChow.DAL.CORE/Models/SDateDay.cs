
using System;
using System.Collections.Generic;

namespace OrderYourChow.DAL.CORE.Models
{
    public class SDateDay
    {
        public SDateDay()
        {
            DDietDays = new HashSet<DDietDay>();
        }
        public int DateDayId { get; set; }
        public int DayId { get; set; }
        public DateTime DateDay { get; set; }

        public virtual SDay SDay { get; set;}
        public virtual ICollection<DDietDay> DDietDays { get; set; }
    }
}
