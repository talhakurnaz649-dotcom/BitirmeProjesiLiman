using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace BitirmeProjesiLiman.Core.Entities
{
    public class Vessels
    {
        public int Id { get; set; }
        public string IMO { get; set; } = string.Empty; // Gemi Benzersiz Kimlik No
        public string Name { get; set; } = string.Empty;
        public double GrossTonnage { get; set; }
        public string Flag { get; set; } = string.Empty; // Bandırası (Ülke)
        public string ShippingCompany { get; set; } = string.Empty;
        // Navigation Properties (Tablo İlişkileri)
        public ICollection<BerthAllocations> BerthAllocations { get; set; } = new List<BerthAllocations>();
        public ICollection<PortInvoices> PortInvoices { get; set; } = new List<PortInvoices>();
    }
}
