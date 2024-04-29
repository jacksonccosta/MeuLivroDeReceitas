using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services;

public class Encripter(string additionalKey)
{
    private readonly string _additionalKey = additionalKey;

    public string Encrypt(string str)
    {
        var newStr = $"{str}{_additionalKey}";
        var bytes = Encoding.UTF8.GetBytes(newStr);
        var hashBytes = SHA512.HashData(bytes);

        return StrBytes(hashBytes);
    }

    private string StrBytes(byte[] bytes)
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
