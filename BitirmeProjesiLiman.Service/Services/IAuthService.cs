using BitirmeProjesiLiman.Core.DTOs;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Service.Services
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginDto loginDto);
        Task<bool> RegisterAsync(RegisterDto registerDto);
    }
}
