using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Core.DTOs
{
    public class YardOccupancyDto
    {
        public int DoluKapasite { get; set; }
        public int ToplamKapasite { get; set; }
        public double DolulukYuzdesi => ToplamKapasite > 0 ? ((double)DoluKapasite / ToplamKapasite) * 100 : 0;
    }
}
