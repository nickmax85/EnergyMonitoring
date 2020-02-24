namespace EnergyMonitoringWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MailDistribution")]
    public partial class MailGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MailDistributionID { get; set; }

        public int GroupID { get; set; }

        public int UserID { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual Group Group { get; set; }

        public virtual User User { get; set; }
    }
}
