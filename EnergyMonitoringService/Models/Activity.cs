using System;
using System.Collections.Generic;

namespace EnergyMonitoringService.Models
{
    public partial class Activity
    {
        public int ActivityId { get; set; }
        public DateTime? Date { get; set; }
        public string Comment { get; set; }
        public int EquipmentId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}
