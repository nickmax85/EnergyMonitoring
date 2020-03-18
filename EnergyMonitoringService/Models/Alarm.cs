using System;
using System.Collections.Generic;

namespace EnergyMonitoringService.Models
{
    public partial class Alarm
    {
        public int AlarmId { get; set; }
        public long RecordId { get; set; }
        public string Remark { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Record Record { get; set; }
    }
}
