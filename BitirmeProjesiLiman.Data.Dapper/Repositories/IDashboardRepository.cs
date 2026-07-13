using BitirmeProjesiLiman.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Data.Dapper.Repositories
{
    public interface IDashboardRepository
    {
        Task<YardOccupancyDto> GetYardOccupancyAsync();
        Task<IEnumerable<SonGemiHareketleriDto>> GetRecentVesselMovementsAsync();
    }
}
