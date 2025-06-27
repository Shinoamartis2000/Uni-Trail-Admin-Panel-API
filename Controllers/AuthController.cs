using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniTrail.Admin.Interfaces;
using UniTrail.Admin.Models;
using UniTrail.Admin.Models.Auth;

namespace UniTrail.Admin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = await _authService.Login(request);
            _logger.LogInformation("User {Username} logged in", request.Username);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning("Failed login attempt for {Username}", request.Username);
            return Unauthorized(ex.Message);
        }
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        try
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _authService.ChangePassword(userId, request.CurrentPassword, request.NewPassword);
            _logger.LogInformation("User {UserId} changed password", userId);
            return Ok(new { Message = "Password changed successfully" });
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "User not found");
            return NotFound(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning("Failed password change attempt");
            return Unauthorized(ex.Message);
        }
    }

    [HttpPost("request-password-reset")]
    public async Task<IActionResult> RequestPasswordReset([FromBody] ResetPasswordRequest request)
    {
        try
        {
            await _authService.ResetPassword(request.Email);
            _logger.LogInformation("Password reset requested for {Email}", request.Email);
            return Ok(new { Message = "If an account exists, a reset email has been sent" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing password reset");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("register-admin")]
    public async Task<ActionResult<User>> RegisterAdmin([FromBody] RegisterRequest request)
    {
        try
        {
            var user = await _authService.Register(request);
            _logger.LogInformation("New admin registered: {Username}", user.Username);
            return CreatedAtAction(nameof(RegisterAdmin), new { id = user.Id }, user);
        }
        catch (ApplicationException ex)
        {
            _logger.LogWarning(ex, "Registration failed");
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpGet("profile")]
    public IActionResult GetProfile()
    {
        var profile = new
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            Username = User.FindFirstValue(ClaimTypes.Name),
            Email = User.FindFirstValue(ClaimTypes.Email),
            Role = User.FindFirstValue(ClaimTypes.Role)
        };
        return Ok(profile);
    }
}