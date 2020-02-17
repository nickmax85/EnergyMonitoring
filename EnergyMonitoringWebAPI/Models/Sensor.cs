namespace EnergyMonitoringWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sensor")]
    public partial class Sensor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sensor()
        {
            Record = new HashSet<Record>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SensorID { get; set; }

        public decimal? LowerLimit { get; set; }

        public decimal? UpperLimit { get; set; }

        public int UnitID { get; set; }

        public int DeviceID { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual Device Device { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Record> Record { get; set; }

        public virtual Unit Unit { get; set; }
    }
}
