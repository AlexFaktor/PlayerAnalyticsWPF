namespace Database.Records;

public enum UserRole
{
    Player,
    Admin
}

public enum GameResult
{
    Win,
    Loss,
    Draw
}

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; }

    public ICollection<PlayerStatistic> PlayerStatistics { get; set; }
    public ICollection<GameSession> GameSessions { get; set; }
    public ICollection<Achievement> Achievements { get; set; }
    public ICollection<Feedback> Feedbacks { get; set; }
}

public class PlayerStatistic
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public int GamesPlayed { get; set; }
    public int GamesWon { get; set; }
    public int GamesDraw { get; set; }
    public double TotalTimePlayed { get; set; } // у годинах
    public int HighScore { get; set; }

    public User User { get; set; }
}

public class GameSession
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public DateTimeOffset SessionDate { get; set; }
    public int Score { get; set; }
    public double Duration { get; set; } // у хвилинах
    public GameResult Result { get; set; }

    public User User { get; set; }
}

public class Achievement
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string AchievementName { get; set; } = string.Empty;
    public DateTimeOffset DateUnlocked { get; set; }

    public User User { get; set; }
}

public class Feedback
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public DateTimeOffset FeedbackDate { get; set; }
    public int Rating { get; set; } // 1-5
    public string Comment { get; set; } = string.Empty;

    public User User { get; set; }
}

public class Report
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid GeneratedBy { get; set; }
    public DateTimeOffset ReportDate { get; set; }
    public string ReportType { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty; // JSON або текст

    public User Admin { get; set; }
}
