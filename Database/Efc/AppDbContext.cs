using Database.Records;
using Microsoft.EntityFrameworkCore;

namespace Database.Efc;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
    public DbSet<GameSession> GameSessions { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerStatistic>()
            .HasOne(ps => ps.User)
            .WithMany(u => u.PlayerStatistics)
            .HasForeignKey(ps => ps.UserId);

        modelBuilder.Entity<GameSession>()
            .HasOne(gs => gs.User)
            .WithMany(u => u.GameSessions)
            .HasForeignKey(gs => gs.UserId);

        modelBuilder.Entity<Achievement>()
            .HasOne(a => a.User)
            .WithMany(u => u.Achievements)
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.User)
            .WithMany(u => u.Feedbacks)
            .HasForeignKey(f => f.UserId);

        modelBuilder.Entity<Report>()
            .HasOne(r => r.Admin)
            .WithMany()
            .HasForeignKey(r => r.GeneratedBy);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={FileHelper.GetDbPath() + "PlayerAnalytics.db"};Foreign Keys=False");
}
