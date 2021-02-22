using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace EnergyMonitoringService.Models
{
    public partial class EnergyMonitoringContext : DbContext
    {
        public EnergyMonitoringContext()
        {
        }

        public EnergyMonitoringContext(DbContextOptions<EnergyMonitoringContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<Alarm> Alarm { get; set; }
        public virtual DbSet<Config> Config { get; set; }
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<Record> Record { get; set; }
        public virtual DbSet<Sensor> Sensor { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.

                IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

                string connectionString = configuration.GetConnectionString("db");
                optionsBuilder.UseSqlServer(connectionString);

                //optionsBuilder.UseSqlServer("Server=localhos;Database=energymonitoring;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer(@"Server=ILZMSCLSQC5\INSTPUB;Database=energymonitoring;User Id=energy_rw;Password=yCkOMk6zkTQ2eUkpZgZg;");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_Equipment");
            });

            modelBuilder.Entity<Alarm>(entity =>
            {
                entity.Property(e => e.AlarmId).HasColumnName("AlarmID");

                entity.Property(e => e.Confirmed).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.Remark)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Record)
                    .WithMany(p => p.Alarm)
                    .HasForeignKey(d => d.RecordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Alarm_Record");
            });

            modelBuilder.Entity<Config>(entity =>
            {
                entity.Property(e => e.ConfigId).HasColumnName("ConfigID");

                entity.Property(e => e.AirPressurePrice).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasIndex(e => e.EquipmentId)
                    .HasName("IX_Device")
                    .IsUnique();

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Equipment)
                    .WithOne(p => p.Device)
                    .HasForeignKey<Device>(d => d.EquipmentId)
                    .HasConstraintName("FK_Device_Equipment");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.InactiveDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(10);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Equipment_Area");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.HasIndex(e => e.SensorId)
                    .HasName("IX_Record");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.SensorId).HasColumnName("SensorID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Value).HasColumnType("decimal(6, 1)");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.Record)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("FK_Record_Equipment");

                entity.HasOne(d => d.Sensor)
                    .WithMany(p => p.Record)
                    .HasForeignKey(d => d.SensorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Record_Sensor");
            });

            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.Property(e => e.SensorId).HasColumnName("SensorID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.LowerLimit).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpperLimit).HasColumnType("decimal(18, 1)");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.Sensor)
                    .HasForeignKey(d => d.DeviceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sensor_Device");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Sensor)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sensor_Unit");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sign).HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
