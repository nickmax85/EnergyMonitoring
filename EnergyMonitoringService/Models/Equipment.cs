using System;
using System.Collections.Generic;

namespace EnergyMonitoringService.Models
{
    public partial class Equipment
    {
        public Equipment()
        {
            Device = new HashSet<Device>();
        }

        public int EquipmentId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public int? LocationId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Device> Device { get; set; }
    }
}
