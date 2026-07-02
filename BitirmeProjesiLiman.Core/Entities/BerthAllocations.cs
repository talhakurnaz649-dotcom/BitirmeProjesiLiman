using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Core.Entities
{
    public class BerthAllocations
    {
        public int Id { get; set; }
        public int VesselId { get; set; }
        public Vessels? Vessel { get; set; } // Gemi ile ilişki

        public int BerthId { get; set; }
        public Berths? Berth { get; set; } // Rıhtım ile ilişki
        public DateTime ArrivalDate { get; set; } // Yanaşma Tarihi
        public DateTime? DepartureDate { get; set; } // Ayrılma Tarihi
        public string Status { get; set; } = "Scheduled"; // Scheduled, Docked, Departed, Cancelled
    }
}
