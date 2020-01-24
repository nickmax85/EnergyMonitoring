using System;
using System.Collections.Generic;

namespace EnergyMonitoringService.Models
{
    public partial class Record
    {
        public long RecordId { get; set; }
        public decimal Value { get; set; }
        public int SensorId { get; set; }
        public int? EquipmentId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual Sensor Sensor { get; set; }
    }
}
