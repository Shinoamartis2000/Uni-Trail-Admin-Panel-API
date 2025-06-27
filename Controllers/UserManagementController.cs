using Microsoft.AspNetCore.Mvc;
using UniTrail.Admin.Interfaces;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Controllers;

[ApiController]
[Route("api/users")]
public class UserManagementController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        return await userService.GetAllUsers();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await userService.GetUserById(id);
        if (user == null) return NotFound();
        return user;
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        var createdUser = await userService.CreateUser(user);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        if (id != user.Id) return BadRequest();

        var updatedUser = await userService.UpdateUser(id, user);
        if (updatedUser == null) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await userService.DeleteUser(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPatch("{id}/activate")]
    public async Task<IActionResult> ToggleUserStatus(int id)
    {
        var result = await userService.ToggleUserStatus(id);
        if (!result) return NotFound();
        return NoContent();
    }
}