using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BitirmeProjesiLiman.Core.Entities
{
    public class Berths
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Rıhtım No (Örn: Rıhtım-A)
        public double DepthCapacity { get; set; } // Su derinliği
        public double LengthCapacity { get; set; } // Uzunluk sınırı
        public decimal HourlyRate { get; set; } // Saatlik ücret tarifesi
        // Navigation Properties
        public ICollection<BerthAllocations> BerthAllocations { get; set; } = new List<BerthAllocations>();
    }
}
