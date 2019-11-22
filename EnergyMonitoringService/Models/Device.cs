using System;
using System.Collections.Generic;

namespace EnergyMonitoringService.Models
{
    public partial class Device
    {
        public Device()
        {
            Sensor = new HashSet<Sensor>();
        }

        public int DeviceId { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public bool? Active { get; set; }
        public int? EquipmentId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual ICollection<Sensor> Sensor { get; set; }
    }
}
