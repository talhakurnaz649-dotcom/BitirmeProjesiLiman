using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BitirmeProjesiLiman.Core.Entities;
using BitirmeProjesiLiman.Core.Repositories;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.EF.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Bu uç noktadaki işlemleri yapabilmek için JWT token ile giriş yapılması zorunludur
    public class ContainersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContainersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var containers = await _unitOfWork.Repository<Containers>().GetAllAsync();
            return Ok(containers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var container = await _unitOfWork.Repository<Containers>().GetByIdAsync(id);
            if (container == null)
                return NotFound("Konteyner bulunamadı.");

            return Ok(container);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Containers container)
        {
            await _unitOfWork.Repository<Containers>().AddAsync(container);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = container.Id }, container);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Containers container)
        {
            if (id != container.Id)
                return BadRequest("ID uyuşmazlığı.");

            _unitOfWork.Repository<Containers>().Update(container);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var container = await _unitOfWork.Repository<Containers>().GetByIdAsync(id);
            if (container == null)
                return NotFound("Konteyner bulunamadı.");

            _unitOfWork.Repository<Containers>().Delete(container);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
