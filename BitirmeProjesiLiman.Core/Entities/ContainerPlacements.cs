using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Core.Entities
{
    public class ContainerPlacements
    {
        public int Id { get; set; }
        public int ContainerId { get; set; }
        public Containers? Container { get; set; }
        public string Block { get; set; } = string.Empty; // Blok Adı (A, B, C, D)
        public int Bay { get; set; } // Yatay Konum
        public int Row { get; set; } // Dikey Konum
        public int Tier { get; set; } // Yükseklik/Kat (1-5)
        public DateTime PlacementDate { get; set; }
    }
}
