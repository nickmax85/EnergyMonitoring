using System;
using System.Collections.Generic;

namespace EnergyMonitoring.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Mail { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual MailDistribution MailDistribution { get; set; }
    }
}
