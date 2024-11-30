using Database.Efc;
using Database.Records;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public interface IRepository<T>
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

public class UserRepository(AppDbContext context) : Repository<UserRecord>(context)
{
    public async Task<List<UserRecord>> GetAllUsersAsync(string? hasInName = null, bool? isAdmin = null)
    {
        return await _dbSet
            .Where(u =>
                (hasInName == null || u.Name.Contains(hasInName)) && // Фільтрація за іменем
                ((isAdmin == null || isAdmin == false) || (isAdmin.Value && u.Role == UserRoles.Admin)) // Фільтрація за роллю
            )
            .ToListAsync();
    }

    public async Task<UserRecord?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Name == name);
    }

    public async Task<UserRecord?> Authorize(string name, string password)
    {
        string hashedPassword = Security.HashPassword(password.Trim());

        var result = await _dbSet.FirstOrDefaultAsync(u => u.Name == name.Trim()
        && u.Password.Trim() == hashedPassword);
        return result;
    }

    public async Task<bool> ChangeName(string oldName, string newName)
    {
        if (oldName == newName)
            return true;

        var possibleUserNew = await GetByNameAsync(newName);

        if (possibleUserNew != null)
            return false;

        var possibleUser = await GetByNameAsync(oldName);

        if (possibleUser == null)
            return false;

        possibleUser.Name = newName;
        _context.SaveChanges();
        return true;
    }   

    public async Task Register(string login, string password)
    {
        var userId = Guid.NewGuid();
        await _dbSet.AddAsync(
            new() { Id = userId, Name = login, Password = Security.HashPassword(password.Trim()), Role = UserRoles.Player });

        await _context.PlayerStatistics.AddAsync(new() { Id = Guid.NewGuid(), UserId = userId, });

        await _context.SaveChangesAsync();
    }
}

public class PlayerStatisticRepository : Repository<PlayerStatisticRecord>
{
    public PlayerStatisticRepository(AppDbContext context) : base(context) { }

    public async Task<PlayerStatisticRecord?> GetByUserId(Guid userId)
    {
        return await _dbSet.FirstOrDefaultAsync(s => s.UserId == userId);
    }

    public async Task ConsiderSession(GameSessionRecord session)
    {
        var stat = await _dbSet.FirstAsync(stat => stat.UserId == session.UserId);
        stat.GamesPlayed++;

        if (session.Result == GameResults.Win)
            stat.GamesWon++;
        else if (session.Result == GameResults.Draw)
            stat.GamesDraw++;

        stat.TotalTimePlayed += Math.Round( session.Duration / 60, 2);

        if (stat.HighScore < session.Score)
            stat.HighScore = session.Score;

        await _context.SaveChangesAsync();
    }
}

public class GameSessionRepository : Repository<GameSessionRecord>
{
    public GameSessionRepository(AppDbContext context) : base(context) { }

    public override async Task AddAsync(GameSessionRecord sessionRecord)
    {
        var statRep = new PlayerStatisticRepository(_context);

        await _dbSet.AddAsync(sessionRecord);
        await statRep.ConsiderSession(sessionRecord);
        await _context.SaveChangesAsync();
    }

    public async Task<List<GameSessionRecord>> GetByUserId(Guid userId)
    {
        return await _dbSet.Where(s => s.UserId == userId).ToListAsync();
    }
}

public class AchievementRepository : Repository<AchievementRecord>
{
    public AchievementRepository(AppDbContext context) : base(context) { }

    public async Task<List<AchievementRecord>> GetByUserId(Guid userId)
    {
        return await _dbSet.Where(s => s.UserId == userId).ToListAsync();
    }
}

public class FeedbackRepository : Repository<FeedbackRecord>
{
    public FeedbackRepository(AppDbContext context) : base(context) { }

    public async Task<FeedbackRecord?> GetByUserId(Guid userId)
    {
        return await _dbSet.FirstOrDefaultAsync(f => f.UserId == userId);
    }

    public async void Save(FeedbackRecord feedback)
    {
        var possibleRecord = await _dbSet.FirstOrDefaultAsync(f => f.UserId == feedback.UserId);
        if (possibleRecord != null)
            _dbSet.Remove(possibleRecord);
        await _dbSet.AddAsync(feedback);
        await _context.SaveChangesAsync();
    }
}

public class ReportRepository : Repository<ReportRecord>
{
    public ReportRepository(AppDbContext context) : base(context) { }
}