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
    
    public partial class Activity
    {
        public int ActivityID { get; set; }
        public string Comment { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public int EquipmentID { get; set; }
    
        public virtual Equipment Equipment { get; set; }
    }
}
