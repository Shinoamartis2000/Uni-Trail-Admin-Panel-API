using Microsoft.EntityFrameworkCore;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<PointOfInterest> PointsOfInterest => Set<PointOfInterest>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Building> Buildings => Set<Building>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PointOfInterest>()
            .HasOne(p => p.BuildingLocation)
            .WithMany(b => b.PointsOfInterest)
            .HasForeignKey(p => p.BuildingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}