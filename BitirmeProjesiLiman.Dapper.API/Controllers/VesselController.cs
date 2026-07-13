using Microsoft.AspNetCore.Mvc;

namespace BitirmeProjesiLiman.Dapper.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VesselController : ControllerBase
    {
        [HttpPost("depart/{id}")]
        public IActionResult Depart(int id)
        {
            // Simply log the departure and return OK as the frontend handles the state transition.
            Console.WriteLine($"Vessel with ID {id} has departed.");
            return Ok(new { message = $"Vessel {id} departed successfully." });
        }
    }
}
