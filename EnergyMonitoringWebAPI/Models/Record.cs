namespace EnergyMonitoringWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Record")]
    public partial class Record
    {
        public long RecordID { get; set; }

        public decimal Value { get; set; }

        public int SensorID { get; set; }

        public int? EquipmentID { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual Equipment Equipment { get; set; }

        public virtual Sensor Sensor { get; set; }
    }
}
