namespace EnergyMonitoringWebAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EnergyMonitoringContext : DbContext
    {
        public EnergyMonitoringContext()
            : base("name=EnergyMonitoringContext")
        {
            //this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;

        }

        public virtual DbSet<Config> Config { get; set; }
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<MailDistribution> MailDistribution { get; set; }
        public virtual DbSet<Record> Record { get; set; }
        public virtual DbSet<Sensor> Sensor { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Config>()
                .Property(e => e.CostPerUnit)
                .HasPrecision(6, 1);

            modelBuilder.Entity<Device>()
                .HasMany(e => e.Sensor)
                .WithRequired(e => e.Device)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Equipment)
                .WithRequired(e => e.Group)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.MailDistribution)
                .WithRequired(e => e.Group)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Record>()
                .Property(e => e.Value)
                .HasPrecision(6, 1);

            modelBuilder.Entity<Sensor>()
                .Property(e => e.LowerLimit)
                .HasPrecision(18, 1);

            modelBuilder.Entity<Sensor>()
                .Property(e => e.UpperLimit)
                .HasPrecision(18, 1);

            modelBuilder.Entity<Sensor>()
                .HasMany(e => e.Record)
                .WithRequired(e => e.Sensor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Sensor)
                .WithRequired(e => e.Unit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Mail)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasOptional(e => e.MailDistribution)
                .WithRequired(e => e.User);
        }
    }
}
