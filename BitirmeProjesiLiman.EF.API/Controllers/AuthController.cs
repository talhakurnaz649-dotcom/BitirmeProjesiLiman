using Microsoft.AspNetCore.Mvc;
using BitirmeProjesiLiman.Core.DTOs;
using BitirmeProjesiLiman.Service.Services;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.EF.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            if (!result)
                return BadRequest("Kullanıcı kaydı başarısız (E-posta adresi kullanılıyor olabilir).");

            return Ok(new { Message = "Kullanıcı kaydı başarıyla oluşturuldu." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            if (token == null)
                return Unauthorized("Geçersiz e-posta adresi veya şifre.");

            return Ok(new { Token = token, Message = "Giriş başarılı." });
        }
    }
}
