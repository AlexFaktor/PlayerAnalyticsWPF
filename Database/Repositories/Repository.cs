using Database.Efc;
using Database.Records;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
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

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
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

public class UserRepository(AppDbContext context) : Repository<UserRecord>(context)
{
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

    public async Task Register(string login, string password)
    {
        await _dbSet.AddAsync(
            new() {Id=Guid.NewGuid(),Name = login, Password = Security.HashPassword(password.Trim()), Role = UserRoles.Player });
        await _context.SaveChangesAsync();
    }
}

public class PlayerStatisticRepository : Repository<PlayerStatisticRecord>
{
    public PlayerStatisticRepository(AppDbContext context) : base(context) { }
}

public class GameSessionRepository : Repository<GameSessionRecord>
{
    public GameSessionRepository(AppDbContext context) : base(context) { }
}

public class AchievementRepository : Repository<AchievementRecord>
{
    public AchievementRepository(AppDbContext context) : base(context) { }
}

public class FeedbackRepository : Repository<FeedbackRecord>
{
    public FeedbackRepository(AppDbContext context) : base(context) { }
}

public class ReportRepository : Repository<ReportRecord>
{
    public ReportRepository(AppDbContext context) : base(context) { }
}