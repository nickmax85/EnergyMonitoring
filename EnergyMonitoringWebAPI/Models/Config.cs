namespace EnergyMonitoringWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Config")]
    public partial class Config
    {
        public int ConfigID { get; set; }

        public int? AuditDay { get; set; }

        public DateTime? AuditTime { get; set; }

        public decimal? CostPerUnit { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
