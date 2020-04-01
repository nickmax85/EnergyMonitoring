using System;
using System.Collections.Generic;

namespace EnergyMonitoringService.Models
{
    public partial class Config
    {
        public int ConfigId { get; set; }
        public int RecordInterval { get; set; }
        public int? AuditDayOfWeek { get; set; }
        public TimeSpan? AuditTimeStart { get; set; }
        public TimeSpan? AuditTimeEnd { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
