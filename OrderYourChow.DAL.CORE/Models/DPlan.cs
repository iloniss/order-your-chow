using System;

#nullable disable

namespace OrderYourChow.DAL.CORE.Models
{
    public partial class DPlan
    {
        public int PlanId { get; set; }
        public int WeekId { get; set; }
        public string Syslog { get; set; }
        public DateTime Sysdate { get; set; }

        public virtual SWeek Week { get; set; }
    }
}
