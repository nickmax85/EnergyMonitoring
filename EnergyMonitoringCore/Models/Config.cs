using System;
using System.Collections.Generic;

namespace EnergyMonitoring.Models
{
    public partial class Config
    {
        public int ConfigId { get; set; }
        public int? AuditDay { get; set; }
        public DateTime? AuditTime { get; set; }
        public decimal? CostPerUnit { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
