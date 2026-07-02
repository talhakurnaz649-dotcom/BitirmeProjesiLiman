using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Core.DTOs
{
    public class VesselReportDto
    {
        public string GemiAdi { get; set; } = string.Empty;
        public string Bayrak { get; set; } = string.Empty;
        public string RihtimAdi { get; set; } = string.Empty;
        public DateTime YanasmaTarihi { get; set; }
        public DateTime? AyrilmaTarihi { get; set; }
        public string Durum { get; set; } = string.Empty;
    }
}
