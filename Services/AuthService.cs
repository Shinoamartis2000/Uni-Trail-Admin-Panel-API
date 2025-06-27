using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UniTrail.Admin.Data;
using UniTrail.Admin.Interfaces;
using UniTrail.Admin.Models;
using UniTrail.Admin.Models.Auth;

namespace UniTrail.Admin.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (user == null || !VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            throw new UnauthorizedAccessException("Invalid credentials");

        if (!user.IsActive)
            throw new UnauthorizedAccessException("Account is disabled");

        user.LastActive = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return new LoginResponse
        {
            Token = GenerateJwtToken(user),
            Expiration = DateTime.UtcNow.AddHours(12),
            Role = user.Role.ToString(),
            Username = user.Username
        };
    }

    public async Task ChangePassword(int userId, string currentPassword, string newPassword)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) throw new KeyNotFoundException("User not found");

        if (!VerifyPasswordHash(currentPassword, user.PasswordHash, user.PasswordSalt))
            throw new UnauthorizedAccessException("Current password is incorrect");

        CreatePasswordHash(newPassword, out var hash, out var salt);

        user.PasswordHash = hash;
        user.PasswordSalt = salt;
        user.LastActive = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task<string> ResetPassword(string email) // Changed to return temp password
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return null;

        var tempPassword = GenerateTemporaryPassword();
        CreatePasswordHash(tempPassword, out var hash, out var salt);

        user.PasswordHash = hash;
        user.PasswordSalt = salt;
        user.LastActive = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return tempPassword; // Return instead of emailing
    }

    public async Task<User> Register(RegisterRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            throw new ApplicationException("Username already exists");

        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            throw new ApplicationException("Email already registered");

        CreatePasswordHash(request.Password, out var hash, out var salt);

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            Role = request.Role,
            PasswordHash = hash,
            PasswordSalt = salt,
            IsActive = true,
            LastActive = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        using var hmac = new HMACSHA512(storedSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(storedHash);
    }

    private static string GenerateTemporaryPassword()
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz23456789";
        return new string(Enumerable.Repeat(chars, 10)
            .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
    }

    Task IAuthService.ResetPassword(string email)
    {
        return ResetPassword(email);
    }
}