using System.Security.Cryptography;
using System.Text;

namespace Database.Efc;

public sealed class Security
{
    public static string HashPassword(string password)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(password);

        byte[] hashBytes = SHA256.HashData(bytes);

        return Convert.ToBase64String(hashBytes);
    }
}