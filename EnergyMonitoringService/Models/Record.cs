using System;
using System.Collections.Generic;

namespace EnergyMonitoringService.Models
{
    public partial class Record
    {
        public Record()
        {
            Alarm = new HashSet<Alarm>();
        }

        public long RecordId { get; set; }
        public decimal Value { get; set; }
        public int SensorId { get; set; }
        public int? EquipmentId { get; set; }
        public int? UnitId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual Sensor Sensor { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<Alarm> Alarm { get; set; }
    }
}
