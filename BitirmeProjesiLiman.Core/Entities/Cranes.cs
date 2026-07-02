using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Core.Entities
{
    public class Cranes
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Vinç Kodu (Örn: Gantry-01)
        public double Capacity { get; set; } // Kaldırma kapasitesi (Ton)
        public string Status { get; set; } = "Active"; // Active, Maintenance (Bakımda), Inactive
    }
}
