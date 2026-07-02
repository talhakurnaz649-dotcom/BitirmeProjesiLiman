using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Core.Entities
{
    public class GateReservations
    {
        public int Id { get; set; }
        public int ContainerId { get; set; }
        public Containers? Container { get; set; }
        public string TruckLicensePlate { get; set; } = string.Empty; // Tır Plakası
        public string DriverName { get; set; } = string.Empty; // Şoför Adı
        public DateTime ScheduledTime { get; set; } // Randevu Tarih/Saati
        public string Direction { get; set; } = "In"; // In (Liman Girişi), Out (Liman Çıkışı)
        public string Status { get; set; } = "Pending"; // Pending, Approved, Completed, Cancelled
    }
}
