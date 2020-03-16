//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EnergyMonitoringWebAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Record
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Record()
        {
            this.Alarms = new HashSet<Alarm>();
        }
    
        public long RecordID { get; set; }
        public decimal Value { get; set; }
        public int SensorID { get; set; }
        public Nullable<int> EquipmentID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alarm> Alarms { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual Sensor Sensor { get; set; }
    }
}
