using Microsoft.EntityFrameworkCore;
using BitirmeProjesiLiman.Core.Entities;
using System;

namespace BitirmeProjesiLiman.Data.EF.Context
{
    public class BitirmeProjesiLimanDbContext : DbContext
    {
        public BitirmeProjesiLimanDbContext(DbContextOptions<BitirmeProjesiLimanDbContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Vessels> Vessels { get; set; }
        public DbSet<Berths> Berths { get; set; }
        public DbSet<BerthAllocations> BerthAllocations { get; set; }
        public DbSet<Containers> Containers { get; set; }
        public DbSet<ContainerPlacements> ContainerPlacements { get; set; }
        public DbSet<GateReservations> GateReservations { get; set; }
        public DbSet<CustomsInspections> CustomsInspections { get; set; }
        public DbSet<Cranes> Cranes { get; set; }
        public DbSet<CraneOperations> CraneOperations { get; set; }
        public DbSet<PortInvoices> PortInvoices { get; set; }
        public DbSet<TelemetryLogs> TelemetryLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // BerthAllocations ilişkileri
            modelBuilder.Entity<BerthAllocations>()
                .HasOne(ba => ba.Vessel)
                .WithMany(v => v.BerthAllocations)
                .HasForeignKey(ba => ba.VesselId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BerthAllocations>()
                .HasOne(ba => ba.Berth)
                .WithMany(b => b.BerthAllocations)
                .HasForeignKey(ba => ba.BerthId)
                .OnDelete(DeleteBehavior.Restrict);

            // PortInvoices ilişkileri
            modelBuilder.Entity<PortInvoices>()
                .HasOne(pi => pi.Vessel)
                .WithMany(v => v.PortInvoices)
                .HasForeignKey(pi => pi.VesselId)
                .OnDelete(DeleteBehavior.Restrict);

            // TelemetryLogs Ayarları
            modelBuilder.Entity<TelemetryLogs>()
                .Property(t => t.SensorValue)
                .HasConversion<double>();

            // Başlangıç Verileri (Seed Data)
            modelBuilder.Entity<Users>().HasData(
                new Users { Id = 1, Username = "admin", Email = "admin@liman.com", PasswordHash = "admin123", Role = "Admin", FullName = "Liman Genel Müdürü" },
                new Users { Id = 2, Username = "operator1", Email = "operator1@liman.com", PasswordHash = "operator123", Role = "Operator", FullName = "Ahmet Vinç Operatörü" },
                new Users { Id = 3, Username = "customs1", Email = "customs1@liman.com", PasswordHash = "customs123", Role = "CustomsOfficer", FullName = "Mehmet Gümrük Muayene Memuru" }
            );

            modelBuilder.Entity<Vessels>().HasData(
                new Vessels { Id = 1, IMO = "IMO9820348", Name = "Poseidon Express", GrossTonnage = 45000, Flag = "Panama", ShippingCompany = "Maersk Line" },
                new Vessels { Id = 2, IMO = "IMO9348209", Name = "Ocean Titan", GrossTonnage = 60000, Flag = "Liberia", ShippingCompany = "MSC" }
            );

            modelBuilder.Entity<Berths>().HasData(
                new Berths { Id = 1, Name = "Rıhtım-1A", DepthCapacity = 16.5, LengthCapacity = 350, HourlyRate = 1200 },
                new Berths { Id = 2, Name = "Rıhtım-2B", DepthCapacity = 14.0, LengthCapacity = 280, HourlyRate = 850 }
            );

            modelBuilder.Entity<Cranes>().HasData(
                new Cranes { Id = 1, Name = "Gantry-V1", Capacity = 65, Status = "Active" },
                new Cranes { Id = 2, Name = "Gantry-V2", Capacity = 80, Status = "Active" }
            );
        }
    }
}
