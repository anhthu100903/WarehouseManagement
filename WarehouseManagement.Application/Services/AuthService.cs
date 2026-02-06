using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WarehouseManagement.Application.DTOs.Auth;
using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Application.Services.IServices;
using WarehouseManagement.Domain.Entities.Identity;

namespace WarehouseManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(IAppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResult> LoginAsync(LoginRequest request)
        {
            var email = request.Email.Trim().ToLower();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email);

            if(user == null)
                return new AuthResult { Success = false, Error = "Email hoặc mật khẩu không đúng" };

            if(user.IsDeleted)
                return new AuthResult { Success = false, Error = "Tài khoản đã bị vô hiệu hóa" };

            var ok = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!ok)
                return new AuthResult { Success = false, Error = "Email hoặc mật khẩu không đúng" };

            var token = GenerateJwtToken(user);

            return new AuthResult { Success = true, UserId = user.Id, Token = token };
        }

        public async Task<AuthResult> RegisterAsync(RegisterRequest request)
        {
            var email = request.Email.Trim().ToLower();

            var exists = await _context.Users.AnyAsync(u => u.Email.ToLower() == email);

            if (exists)
                return new AuthResult { Success = false, Error = "Email đã tồn tại" };

            var user = new User
            {
                Email = email,
                FullName = request.FullName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Phone = request.Phone,
                IsDeleted = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user);

            return new AuthResult { Success = true, UserId = user.Id, Token = token };
        }

        private string GenerateJwtToken(User user)
        {
            var secret = _configuration["Jwt:Secret"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            if (string.IsNullOrEmpty(secret))
                throw new Exception("Jwt:Secret chưa được cấu hình");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("fullName", user.FullName)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
