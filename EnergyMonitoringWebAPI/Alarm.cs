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
    
    public partial class Alarm
    {
        public int AlarmID { get; set; }
        public long RecordID { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> Confirmed { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    
        public virtual Record Record { get; set; }
    }
}
