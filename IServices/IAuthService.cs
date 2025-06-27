using Microsoft.AspNetCore.Identity.Data;
using UniTrail.Admin.Models;
using UniTrail.Admin.Models.Auth;
using RegisterRequest = UniTrail.Admin.Models.Auth.RegisterRequest;

namespace UniTrail.Admin.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> Login(Models.Auth.LoginRequest request);
    Task ChangePassword(int userId, string currentPassword, string newPassword);
    Task ResetPassword(string email);
    Task<User> Register(RegisterRequest request);
}