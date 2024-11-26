using Database.Records;
using Microsoft.EntityFrameworkCore;

namespace Database.Efc;

public class AppDbContext : DbContext
{
    public DbSet<UserRecord> Users { get; set; }
    public DbSet<PlayerStatisticRecord> PlayerStatistics { get; set; }
    public DbSet<GameSessionRecord> GameSessions { get; set; }
    public DbSet<AchievementRecord> Achievements { get; set; }
    public DbSet<FeedbackRecord> Feedbacks { get; set; }
    public DbSet<ReportRecord> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerStatisticRecord>()
            .HasOne(ps => ps.User)
            .WithMany(u => u.PlayerStatistics)
            .HasForeignKey(ps => ps.UserId);

        modelBuilder.Entity<GameSessionRecord>()
            .HasOne(gs => gs.User)
            .WithMany(u => u.GameSessions)
            .HasForeignKey(gs => gs.UserId);

        modelBuilder.Entity<AchievementRecord>()
            .HasOne(a => a.User)
            .WithMany(u => u.Achievements)
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<FeedbackRecord>()
            .HasOne(f => f.User)
            .WithMany(u => u.Feedbacks)
            .HasForeignKey(f => f.UserId);

        modelBuilder.Entity<ReportRecord>()
            .HasOne(r => r.Creator)
            .WithMany()
            .HasForeignKey(r => r.GeneratedBy);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = FileHelper.GetExePath() + "PlayerAnalytics.db";
        optionsBuilder.UseSqlite($"Data Source={dbPath};Foreign Keys=False");
    }
}
