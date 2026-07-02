using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BitirmeProjesiLiman.Core.Entities
{
    public class Containers
    {
        public int Id { get; set; }
        public string ContainerNumber { get; set; } = string.Empty; // Örn: MSKU9081234
        public string Size { get; set; } = "20ft"; // 20ft, 40ft
        public string CargoType { get; set; } = "Dry"; // Dry, Reefer (Soğutmalı), Hazardous (Tehlikeli)
        public double TemperatureTarget { get; set; } // Soğutmalı ise hedef sıcaklık derecesi
        public double Weight { get; set; } // Tonajı
        public string Status { get; set; } = "InYard"; // InVessel, InYard, Discharged, GateOut
        // Navigation Properties
        public ICollection<ContainerPlacements> Placements { get; set; } = new List<ContainerPlacements>();
        public ICollection<GateReservations> GateReservations { get; set; } = new List<GateReservations>();
        public ICollection<CustomsInspections> CustomsInspections { get; set; } = new List<CustomsInspections>();
        public ICollection<CraneOperations> CraneOperations { get; set; } = new List<CraneOperations>();
    }
}
