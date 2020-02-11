using System;
using System.Collections.Generic;

namespace EnergyMonitoring.Models
{
    public partial class Group
    {
        public Group()
        {
            Equipment = new HashSet<Equipment>();
            MailDistribution = new HashSet<MailDistribution>();
        }

        public int GroupId { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<MailDistribution> MailDistribution { get; set; }
    }
}
