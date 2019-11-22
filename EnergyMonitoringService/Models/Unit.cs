using System;
using System.Collections.Generic;

namespace EnergyMonitoringService.Models
{
    public partial class Unit
    {
        public Unit()
        {
            Sensor = new HashSet<Sensor>();
        }

        public int UnitId { get; set; }
        public string Name { get; set; }
        public string Sign { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Sensor> Sensor { get; set; }
    }
}
