using Microsoft.AspNetCore.Mvc;
using BitirmeProjesiLiman.Core.DTOs;
using BitirmeProjesiLiman.Service.Services;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.EF.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController : ControllerBase
    {
        private readonly IAIService _aiService;

        public AIController(IAIService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] ChatRequestDto requestDto)
        {
            if (string.IsNullOrWhiteSpace(requestDto.Message))
                return BadRequest("Mesaj boş olamaz.");

            var answer = await _aiService.AskGeminiAsync(requestDto.Message);
            return Ok(new { Answer = answer });
        }
    }
}
