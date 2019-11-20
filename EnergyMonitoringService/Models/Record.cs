using System;
using System.Collections.Generic;

namespace EnergyMonitoringService.Models
{
    public partial class Record
    {
        public int RecordId { get; set; }
        public float Value { get; set; }
        public int SensorId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual Sensor Sensor { get; set; }
    }
}
