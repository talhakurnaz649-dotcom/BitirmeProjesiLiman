using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Core.Entities
{
    public class PortInvoices
    {
        public int Id { get; set; }
        public int VesselId { get; set; }
        public Vessels? Vessel { get; set; }
        public int BerthAllocationId { get; set; }
        public BerthAllocations? BerthAllocation { get; set; }
        public decimal TotalAmount { get; set; } // Toplam fatura tutarı
        public string PaymentStatus { get; set; } = "Unpaid"; // Unpaid (Ödenmedi), Paid (Ödendi), Overdue (Gecikmiş)
        public DateTime InvoiceDate { get; set; }
    }
}
