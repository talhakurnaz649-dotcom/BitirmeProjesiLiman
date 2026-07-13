using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BitirmeProjesiLiman.Core.Entities;
using BitirmeProjesiLiman.Core.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.EF.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GateController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GateController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var reservations = await _unitOfWork.Repository<GateReservations>().GetAllAsync();
            return Ok(reservations.OrderBy(r => r.Id));
        }

        [HttpPost("reservation")]
        public async Task<IActionResult> CreateReservation([FromBody] GateReservations reservation)
        {
            if (reservation.ScheduledTime == default)
            {
                reservation.ScheduledTime = DateTime.UtcNow.AddHours(24);
            }
            if (string.IsNullOrEmpty(reservation.Status))
            {
                reservation.Status = "Pending";
            }

            await _unitOfWork.Repository<GateReservations>().AddAsync(reservation);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { Message = "Tır randevusu başarıyla oluşturuldu.", ReservationId = reservation.Id, reservation });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GateReservations reservation)
        {
            var existing = await _unitOfWork.Repository<GateReservations>().GetByIdAsync(id);
            if (existing == null)
                return NotFound("Randevu bulunamadı.");

            existing.TruckLicensePlate = reservation.TruckLicensePlate;
            existing.DriverName = reservation.DriverName;
            existing.ScheduledTime = reservation.ScheduledTime;
            existing.Direction = reservation.Direction;
            existing.Status = reservation.Status;

            _unitOfWork.Repository<GateReservations>().Update(existing);
            await _unitOfWork.SaveChangesAsync();

            return Ok(existing);
        }

        [HttpPut("approve/{id}")]
        [Authorize(Roles = "Admin,Operator")] // Sadece Admin ve Operatör onaylayabilir
        public async Task<IActionResult> ApproveReservation(int id)
        {
            var reservation = await _unitOfWork.Repository<GateReservations>().GetByIdAsync(id);
            if (reservation == null)
                return NotFound("Randevu bulunamadı.");

            reservation.Status = "Approved";
            _unitOfWork.Repository<GateReservations>().Update(reservation);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { Message = "Randevu onaylandı. Tır limana giriş yapabilir.", reservation });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _unitOfWork.Repository<GateReservations>().GetByIdAsync(id);
            if (reservation == null)
                return NotFound("Randevu bulunamadı.");

            _unitOfWork.Repository<GateReservations>().Delete(reservation);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
