using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BitirmeProjesiLiman.Core.Entities;
using BitirmeProjesiLiman.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.EF.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomsInspections inspection)
        {
            inspection.Status = "Pending";
            await _unitOfWork.Repository<CustomsInspections>().AddAsync(inspection);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { Message = "Gümrük muayene kaydı oluşturuldu.", InspectionId = inspection.Id });
        }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] string newStatus, [FromQuery] string notes)
        {
            var inspection = await _unitOfWork.Repository<CustomsInspections>().GetByIdAsync(id);
            if (inspection == null)
                return NotFound("Muayene kaydı bulunamadı.");

            inspection.Status = newStatus; // Cleared, Flagged, Hold
            inspection.InspectionNotes = notes;

            _unitOfWork.Repository<CustomsInspections>().Update(inspection);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { Message = $"Gümrük durumu güncellendi: {newStatus}" });
        }
    }
}
