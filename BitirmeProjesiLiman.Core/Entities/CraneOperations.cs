using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Core.Entities
{
    public class CraneOperations
    {
        public int Id { get; set; }
        public int CraneId { get; set; }
        public Cranes? Crane { get; set; }
        public int ContainerId { get; set; }
        public Containers? Container { get; set; }
        public int OperatorUserId { get; set; } // Vinci yöneten operatör (Users.Id)
        public Users? OperatorUser { get; set; }
        public DateTime StartTime { get; set; } // Operasyon başlangıç
        public DateTime? EndTime { get; set; } // Operasyon bitiş
        public string OperationType { get; set; } = "Loading"; // Loading, Discharge, Shifting (Saha içi taşıma)
    }
}
