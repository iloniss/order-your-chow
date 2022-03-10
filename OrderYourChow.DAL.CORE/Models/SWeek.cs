using System;
using System.Collections.Generic;

#nullable disable

namespace OrderYourChow.DAL.CORE.Models
{
    public partial class SWeek
    {
        public SWeek()
        {
            DPlans = new HashSet<DPlan>();
        }

        public int WeekId { get; set; }
        public int Week { get; set; }
        public int Year { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Syslog { get; set; }
        public DateTime Sysdate { get; set; }

        public virtual ICollection<DPlan> DPlans { get; set; }
    }
}
