using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services;

public static class Encripter
{
    public static string Encrypt(string str)
    {
        var additinalKey = "A1B2C3D4E5";
        var newStr = $"{str}{additinalKey}";
        var bytes = Encoding.UTF8.GetBytes(newStr);
        var hashBytes = SHA512.HashData(bytes);

        return StrBytes(hashBytes);
    }

    private static string StrBytes(byte[] bytes)
    {
        var sb = new StringBuilder();

        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }
}
