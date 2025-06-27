using Microsoft.EntityFrameworkCore;
using UniTrail.Admin.Data;
using UniTrail.Admin.Interfaces;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Services;

public class UserService(AppDbContext context) : IUserService
{
    public async Task<List<User>> GetAllUsers()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetUserById(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<User> CreateUser(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateUser(int id, User user)
    {
        var existingUser = await context.Users.FindAsync(id);
        if (existingUser == null) return null;

        existingUser.Username = user.Username;
        existingUser.Email = user.Email;
        existingUser.Role = user.Role;
        existingUser.IsActive = user.IsActive;

        await context.SaveChangesAsync();
        return existingUser;
    }

    public async Task<bool> DeleteUser(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return false;

        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleUserStatus(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return false;

        user.IsActive = !user.IsActive;
        await context.SaveChangesAsync();
        return true;
    }
}