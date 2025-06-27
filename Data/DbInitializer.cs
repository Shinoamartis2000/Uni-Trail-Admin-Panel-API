using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        // Only seed if database is empty
        if (context.Users.Any()) return;

        using var transaction = context.Database.BeginTransaction();

        try
        {
            SeedBuildings(context);
            SeedUsers(context);
            SeedPointsOfInterest(context);
            SeedEvents(context);
            SeedActivityLogs(context);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private static void SeedBuildings(AppDbContext context)
    {
        if (context.Buildings.Any()) return;

        var buildings = new List<Building>
    {
        new() { 
            // Don't set Id - let database generate it
            Name = "Main Library",
            Code = "LIB",
            Description = "Central university library",
            Latitude = 34.052235,
            Longitude = -118.243683,
            Address = "100 Library Way",
            IsActive = true
        },
        new() { 
            // Don't set Id - let database generate it
            Name = "Science Center",
            Code = "SCI",
            Description = "Science and research complex",
            Latitude = 34.053235,
            Longitude = -118.244683,
            Address = "200 Science Blvd",
            IsActive = true
        }
    };

        context.Buildings.AddRange(buildings);
        context.SaveChanges();
    }

    private static void SeedUsers(AppDbContext context)
    {
        if (context.Users.Any()) return;

        CreatePasswordHash("admin123", out var adminHash, out var adminSalt);
        CreatePasswordHash("professor123", out var profHash, out var profSalt);
        CreatePasswordHash("student123", out var studentHash, out var studentSalt);

        var users = new List<User>
        {
            new() {
                Username = "admin",
                Email = "admin@unitrail.com",
                Role = UserRole.Admin,
                PasswordHash = adminHash,
                PasswordSalt = adminSalt,
                IsActive = true,
                LastActive = DateTime.UtcNow
            },
            new() {
                Username = "professorX",
                Email = "profx@university.edu",
                Role = UserRole.Staff,
                PasswordHash = profHash,
                PasswordSalt = profSalt,
                IsActive = true,
                LastActive = DateTime.UtcNow
            },
            new() {
                Username = "student123",
                Email = "student123@university.edu",
                Role = UserRole.Student,
                PasswordHash = studentHash,
                PasswordSalt = studentSalt,
                IsActive = true,
                LastActive = DateTime.UtcNow
            }
        };

        context.Users.AddRange(users);
        context.SaveChanges();
    }


    private static void SeedPointsOfInterest(AppDbContext context)
    {
        if (context.PointsOfInterest.Any()) return;

        var buildings = context.Buildings.AsNoTracking().ToList();
        if (buildings.Count < 2) return;

        var pointsOfInterest = new List<PointOfInterest>
        {
            new() {
                Name = "Library Main Entrance",
                Description = "Primary entrance to the library",
                BuildingId = buildings[0].Id,
                IsActive = true
            },
            new() {
                Name = "Science Lab A",
                Description = "Chemistry research lab",
                BuildingId = buildings[1].Id,
                IsActive = true
            }
        };

        context.PointsOfInterest.AddRange(pointsOfInterest);
        context.SaveChanges();
    }

    private static void SeedEvents(AppDbContext context)
    {
        if (context.Events.Any()) return;

        var events = new List<Event>
        {
            new() {
                Title = "Campus Orientation",
                Description = "New student orientation tour",
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(1).AddHours(2),
                Location = "Main Quad",
                Organizer = "professorX"
            },
            new() {
                Title = "Science Fair",
                Description = "Annual student research exhibition",
                StartTime = DateTime.Now.AddDays(3),
                EndTime = DateTime.Now.AddDays(3).AddHours(4),
                Location = "Science Center",
                Organizer = "admin"
            }
        };

        context.Events.AddRange(events);
        context.SaveChanges();
    }

    private static void SeedActivityLogs(AppDbContext context)
    {
        if (context.ActivityLogs.Any()) return;

        var activityLogs = new List<ActivityLog>
        {
            new() {
                Timestamp = DateTime.Now.AddMinutes(-30),
                UserId = "student123",
                ActivityType = "Navigation",
                Description = "Navigated to Library",
                Location = "Main Library",
                Status = "Completed"
            },
            new() {
                Timestamp = DateTime.Now.AddMinutes(-45),
                UserId = "professorX",
                ActivityType = "QR Scan",
                Description = "QR Code Scan at Science Building",
                Location = "Science Center",
                Status = "Completed"
            }
        };

        context.ActivityLogs.AddRange(activityLogs);
        context.SaveChanges();
    }

    private static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
}