using System;

namespace BitirmeProjesiLiman.Core.DTOs
{
    public class SonGemiHareketleriDto
    {
        public string GemiAdi { get; set; } = string.Empty;
        public string Bayrak { get; set; } = string.Empty;
        public string RihtimAdi { get; set; } = string.Empty;
        public DateTime YanasmaTarihi { get; set; }
        public string Durum { get; set; } = string.Empty;
    }
}
