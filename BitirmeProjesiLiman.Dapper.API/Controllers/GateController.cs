using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using BitirmeProjesiLiman.Core.Entities;

namespace BitirmeProjesiLiman.Dapper.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GateController : ControllerBase
    {
        private static readonly ConcurrentDictionary<int, GateReservations> Reservations = new();
        private static int _nextId = 3;

        static GateController()
        {
            Reservations[1] = new GateReservations
            {
                Id = 1,
                ContainerId = 1,
                TruckLicensePlate = "34 ABC 123",
                DriverName = "Ahmet Yurt",
                ScheduledTime = DateTime.Parse("2026-07-06T16:00:00"),
                Direction = "In",
                Status = "Approved"
            };

            Reservations[2] = new GateReservations
            {
                Id = 2,
                ContainerId = 2,
                TruckLicensePlate = "06 XYZ 99",
                DriverName = "Mehmet Kaya",
                ScheduledTime = DateTime.Parse("2026-07-06T18:30:00"),
                Direction = "Out",
                Status = "Pending"
            };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Reservations.Values.OrderBy(r => r.Id));
        }

        [HttpPost("reservation")]
        public IActionResult Create([FromBody] GateReservations reservation)
        {
            if (reservation == null)
            {
                return BadRequest();
            }

            int id = Interlocked.Increment(ref _nextId) - 1;
            reservation.Id = id;
            if (string.IsNullOrEmpty(reservation.Status))
            {
                reservation.Status = "Pending";
            }
            
            Reservations[id] = reservation;
            return Ok(reservation);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] GateReservations reservation)
        {
            if (reservation == null || !Reservations.ContainsKey(id))
            {
                return NotFound();
            }

            reservation.Id = id;
            Reservations[id] = reservation;
            return Ok(reservation);
        }

        [HttpPut("approve/{id}")]
        public IActionResult Approve(int id)
        {
            if (!Reservations.TryGetValue(id, out var reservation))
            {
                return NotFound();
            }

            reservation.Status = "Approved";
            return Ok(reservation);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (Reservations.TryRemove(id, out var reservation))
            {
                return Ok(reservation);
            }
            return NotFound();
        }
    }
}
