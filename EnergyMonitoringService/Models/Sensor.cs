using System;
using System.Collections.Generic;

namespace EnergyMonitoringService.Models
{
    public partial class Sensor
    {
        public Sensor()
        {
            Record = new HashSet<Record>();
        }

        public int SensorId { get; set; }
        public decimal? LowerLimit { get; set; }
        public decimal? UpperLimit { get; set; }
        public int UnitId { get; set; }
        public int DeviceId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Device Device { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<Record> Record { get; set; }
    }
}
