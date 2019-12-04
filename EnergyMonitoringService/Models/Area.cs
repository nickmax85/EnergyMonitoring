using System;
using System.Collections.Generic;

namespace EnergyMonitoringService.Models
{
    public partial class Area
    {
        public Area()
        {
            Equipment = new HashSet<Equipment>();
        }

        public int AreaId { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
