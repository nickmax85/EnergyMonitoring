using System;
using System.Collections.Generic;

namespace EnergyMonitoringService.Models
{
    public partial class MailGroup
    {
        public int MailDistributionId { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Group Group { get; set; }
        public virtual User MailDistribution { get; set; }
    }
}
