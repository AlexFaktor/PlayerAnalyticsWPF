namespace Database.Records;

public enum UserRoles
{
    Player,
    Admin
}

public enum GameResults
{
    Win,
    Loss,
    Draw
}

public class UserRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRoles Role { get; set; }

    public ICollection<PlayerStatisticRecord> PlayerStatistics { get; set; }
    public ICollection<GameSessionRecord> GameSessions { get; set; }
    public ICollection<AchievementRecord> Achievements { get; set; }
    public ICollection<FeedbackRecord> Feedbacks { get; set; }
}

public class PlayerStatisticRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public int GamesPlayed { get; set; }
    public int GamesWon { get; set; }
    public int GamesDraw { get; set; }
    public double TotalTimePlayed { get; set; } // у годинах
    public int HighScore { get; set; }

    public UserRecord User { get; set; }
}

public class GameSessionRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public DateTimeOffset SessionDate { get; set; }
    public int Score { get; set; }
    public double Duration { get; set; } // у хвилинах
    public GameResults Result { get; set; }

    public UserRecord User { get; set; }
}

public class AchievementRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string AchievementName { get; set; } = string.Empty;
    public DateTimeOffset DateUnlocked { get; set; }

    public UserRecord User { get; set; }
}

public class FeedbackRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public DateTimeOffset FeedbackDate { get; set; }
    public int Rating { get; set; } // 1-5
    public string Comment { get; set; } = string.Empty;

    public UserRecord User { get; set; }
}

public class ReportRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid GeneratedBy { get; set; }
    public DateTimeOffset ReportDate { get; set; }
    public string ReportType { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty; // JSON або текст

    public UserRecord Creator { get; set; }
}
