namespace Database.Efc;

public static class DbInitializer
{
    public static void InitializeDatabase(AppDbContext context)
    {
        // Перевіряємо, чи існує база даних
        var dbPath = FileHelper.GetExePath() + "PlayerAnalytics.db";

        if (!File.Exists(dbPath))
        {
            context.Database.EnsureCreated(); // Створюємо базу, якщо її немає
        }
    }
}
