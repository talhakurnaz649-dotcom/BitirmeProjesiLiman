using Dapper;
using Microsoft.Data.Sqlite;
using BitirmeProjesiLiman.Core.DTOs;
using BitirmeProjesiLiman.Data.Dapper.Connection;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Data.Dapper.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public DashboardRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<YardOccupancyDto> GetYardOccupancyAsync()
        {
            using IDbConnection db = _dbConnectionFactory.CreateConnection();
            
            const string sql = @"
                SELECT 
                    (SELECT COUNT(*) FROM ContainerPlacements) AS DoluKapasite,
                    5000 AS ToplamKapasite;
            ";
            
            var result = await db.QueryFirstOrDefaultAsync<YardOccupancyDto>(sql);
            return result ?? new YardOccupancyDto { DoluKapasite = 0, ToplamKapasite = 5000 };
        }

        public async Task<IEnumerable<SonGemiHareketleriDto>> GetRecentVesselMovementsAsync()
        {
            using IDbConnection db = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT 
                    v.Name AS GemiAdi,
                    v.Flag AS Bayrak,
                    b.Name AS RihtimAdi,
                    ba.ArrivalDate AS YanasmaTarihi,
                    ba.Status AS Durum
                FROM BerthAllocations ba
                INNER JOIN Vessels v ON ba.VesselId = v.Id
                INNER JOIN Berths b ON ba.BerthId = b.Id
                ORDER BY ba.ArrivalDate DESC
                LIMIT 5;
            ";

            return await db.QueryAsync<SonGemiHareketleriDto>(sql);
        }
    }
}
