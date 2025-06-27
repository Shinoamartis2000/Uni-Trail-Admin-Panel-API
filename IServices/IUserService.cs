// Interfaces/IUserService.cs
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllUsers();
    Task<User?> GetUserById(int id);
    Task<User> CreateUser(User user);
    Task<User?> UpdateUser(int id, User user);
    Task<bool> DeleteUser(int id);
    Task<bool> ToggleUserStatus(int id);
}
