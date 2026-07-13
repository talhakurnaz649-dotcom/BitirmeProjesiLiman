using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BitirmeProjesiLiman.Data.Dapper.Repositories;
using BitirmeProjesiLiman.Service.Caching;
using System;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Dapper.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // JWT Token ile korunuyor
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly ICacheService _cacheService;

        public DashboardController(IDashboardRepository dashboardRepository, ICacheService cacheService)
        {
            _dashboardRepository = dashboardRepository;
            _cacheService = cacheService;
        }

        [HttpGet("occupancy")]
        public async Task<IActionResult> GetOccupancy()
        {
            const string cacheKey = "YardOccupancy";
            var cachedData = _cacheService.Get<object>(cacheKey);

            if (cachedData != null)
                return Ok(cachedData);

            var occupancy = await _dashboardRepository.GetYardOccupancyAsync();
            _cacheService.Set(cacheKey, occupancy, TimeSpan.FromMinutes(5));

            return Ok(occupancy);
        }

        [HttpGet("recent-vessels")]
        public async Task<IActionResult> GetRecentVessels()
        {
            var movements = await _dashboardRepository.GetRecentVesselMovementsAsync();
            return Ok(movements);
        }
    }
}
