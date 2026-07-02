using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Core.Entities
{
    public class CustomsInspections
    {
        public int Id { get; set; }
        public int ContainerId { get; set; }
        public Containers? Container { get; set; }
        public int OfficerUserId { get; set; } // Muayeneyi yapan Gümrük Memuru (Users.Id)
        public Users? OfficerUser { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Cleared (Serbest), Flagged (Riskli), Hold (Beklemede)
        public string InspectionNotes { get; set; } = string.Empty; // Gümrük muayene notları
    }
}
