using Database.Efc;
using Database.Records;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
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

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
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

public class UserRepository : Repository<User>
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Name == name);
    }
}

public class PlayerStatisticRepository : Repository<PlayerStatistic>
{
    public PlayerStatisticRepository(AppDbContext context) : base(context) { }
}

public class GameSessionRepository : Repository<GameSession>
{
    public GameSessionRepository(AppDbContext context) : base(context) { }
}

public class AchievementRepository : Repository<Achievement>
{
    public AchievementRepository(AppDbContext context) : base(context) { }
}

public class FeedbackRepository : Repository<Feedback>
{
    public FeedbackRepository(AppDbContext context) : base(context) { }
}

public class ReportRepository : Repository<Report>
{
    public ReportRepository(AppDbContext context) : base(context) { }
}