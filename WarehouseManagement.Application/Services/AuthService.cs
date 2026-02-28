using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
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
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public AuthService(IAppDbContext context, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> LoginAsync(LoginRequest request)
        {
            var email = request.Email.Trim().ToLower();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email);

            if (user == null || user.IsDeleted)
                return new AuthResult { Success = false, Error = "Email hoặc mật khẩu không đúng" };

            var ok = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);
            if (!ok)
                return new AuthResult { Success = false, Error = "Email hoặc mật khẩu không đúng" };

            var token = _tokenService.GenerateToken(user);

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
                Id = Guid.NewGuid(),
                Email = email,
                FullName = request.FullName,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                Phone = request.Phone,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _tokenService.GenerateToken(user);

            return new AuthResult { Success = true, UserId = user.Id, Token = token };
        }
    }
}
