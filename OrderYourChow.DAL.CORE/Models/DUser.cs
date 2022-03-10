using System;
using System.Collections.Generic;

namespace OrderYourChow.DAL.CORE.Models
{
    public class DUser
    {
        public DUser()
        {
            DDietDays = new HashSet<DDietDay>();
        }
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public int MultiplierDiet { get; set; }
        public string Syslog { get; set; }
        public DateTime Sysdate { get; set; }

        public virtual ICollection<DDietDay> DDietDays { get; set; }
    }

    

}
