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

            modelBuilder.Entity<BerthAllocations>().HasData(
                new BerthAllocations { Id = 1, VesselId = 1, BerthId = 1, ArrivalDate = DateTime.Now.AddDays(-3), DepartureDate = DateTime.Now.AddDays(1), Status = "Docked" },
                new BerthAllocations { Id = 2, VesselId = 2, BerthId = 2, ArrivalDate = DateTime.Now.AddDays(-1), DepartureDate = DateTime.Now.AddDays(3), Status = "Docked" }
            );

            modelBuilder.Entity<Containers>().HasData(
                new Containers { Id = 1, ContainerNumber = "MSKU9081234", Size = "40ft", CargoType = "Dry", Weight = 24.5, Status = "InYard" },
                new Containers { Id = 2, ContainerNumber = "MSC8872130", Size = "20ft", CargoType = "Reefer", TemperatureTarget = -18.0, Weight = 18.2, Status = "InYard" },
                new Containers { Id = 3, ContainerNumber = "APLU3421098", Size = "40ft", CargoType = "Hazardous", Weight = 30.0, Status = "InYard" }
            );

            modelBuilder.Entity<ContainerPlacements>().HasData(
                new ContainerPlacements { Id = 1, ContainerId = 1, Block = "A", Bay = 2, Row = 3, Tier = 1, PlacementDate = DateTime.Now.AddDays(-2) },
                new ContainerPlacements { Id = 2, ContainerId = 2, Block = "B", Bay = 1, Row = 4, Tier = 2, PlacementDate = DateTime.Now.AddDays(-1) },
                new ContainerPlacements { Id = 3, ContainerId = 3, Block = "C", Bay = 3, Row = 1, Tier = 1, PlacementDate = DateTime.Now.AddDays(-3) }
            );

            modelBuilder.Entity<GateReservations>().HasData(
                new GateReservations { Id = 1, ContainerId = 1, TruckLicensePlate = "34 ABC 123", DriverName = "Ahmet Yurt", ScheduledTime = DateTime.Now.AddHours(2), Direction = "In", Status = "Approved" },
                new GateReservations { Id = 2, ContainerId = 2, TruckLicensePlate = "06 XYZ 99", DriverName = "Mehmet Kaya", ScheduledTime = DateTime.Now.AddHours(6), Direction = "Out", Status = "Pending" }
            );

            modelBuilder.Entity<CustomsInspections>().HasData(
                new CustomsInspections { Id = 1, ContainerId = 2, OfficerUserId = 3, Status = "Cleared", InspectionNotes = "Sıcaklık hedefleri normal, kargo içeriği temiz." }
            );

            modelBuilder.Entity<PortInvoices>().HasData(
                new PortInvoices { Id = 1, VesselId = 1, BerthAllocationId = 1, TotalAmount = 24500, PaymentStatus = "Unpaid", InvoiceDate = DateTime.Now.AddDays(-1) }
            );
        }
    }
}
