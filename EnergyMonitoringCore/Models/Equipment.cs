﻿using System;
using System.Collections.Generic;

namespace EnergyMonitoring.Models
{
    public partial class Equipment
    {
        public Equipment()
        {
            Device = new HashSet<Device>();
            Record = new HashSet<Record>();
        }

        public int EquipmentId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<Device> Device { get; set; }
        public virtual ICollection<Record> Record { get; set; }
    }
}