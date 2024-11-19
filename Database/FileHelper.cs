using System.Reflection;

namespace Database;

public class FileHelper
{
    public static string GetExePath()
    {
        string exePath = Assembly.GetExecutingAssembly().Location;

        return exePath;
    }

    public static string GetDbPath()
    {
        string localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string localDbPath = Path.Combine(localAppDataFolder, "PlayerAnalytics");

        return localDbPath;
    }
}