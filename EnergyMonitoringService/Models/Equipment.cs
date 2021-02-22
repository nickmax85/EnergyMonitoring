using System;
using System.Collections.Generic;

namespace EnergyMonitoringService.Models
{
    public partial class Equipment
    {
        public Equipment()
        {
            Activity = new HashSet<Activity>();
            Device = new HashSet<Device>();
            Record = new HashSet<Record>();
        }

        public int EquipmentId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public int GroupId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? InactiveDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<Activity> Activity { get; set; }
        public virtual ICollection<Device> Device { get; set; }
        public virtual ICollection<Record> Record { get; set; }
    }
}
