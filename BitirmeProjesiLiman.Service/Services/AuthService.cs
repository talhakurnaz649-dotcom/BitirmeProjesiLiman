using Microsoft.IdentityModel.Tokens;
using BitirmeProjesiLiman.Core.DTOs;
using BitirmeProjesiLiman.Core.Entities;
using BitirmeProjesiLiman.Core.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace BitirmeProjesiLiman.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _jwtSecret = "PortMasterSuperSecretKey1234567890!!!"; // Gerçek projede appsettings'te saklanır

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string?> LoginAsync(LoginDto loginDto)
        {
            var users = await _unitOfWork.Repository<Users>().GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email == loginDto.Email);

            // Hem hash'lenmiş şifreyi hem de seed datadaki düz metin şifreyi (örn: admin123) kabul edebilmesi için çift kontrol yapıyoruz.
            if (user == null || (user.PasswordHash != loginDto.Password && user.PasswordHash != HashPassword(loginDto.Password)))
                return null; // Kullanıcı bulunamadı veya şifre yanlış

            return GenerateJwtToken(user);
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            var users = await _unitOfWork.Repository<Users>().GetAllAsync();
            if (users.Any(u => u.Email == registerDto.Email))
                return false; // E-posta zaten kullanımda

            var newUser = new Users
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = HashPassword(registerDto.Password),
                FullName = registerDto.FullName,
                Role = registerDto.Role
            };

            await _unitOfWork.Repository<Users>().AddAsync(newUser);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private string GenerateJwtToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7), // Token 7 gün geçerli
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
