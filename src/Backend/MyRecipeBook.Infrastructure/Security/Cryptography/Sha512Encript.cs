﻿using MyRecipeBook.Domain;
using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Infrastructure.Security;

public class Sha512Encript(string additionalKey) : IPasswordEncripter
{
    private readonly string _additionalKey = additionalKey;

    public string Encrypt(string str)
    {
        var newStr = $"{str}{_additionalKey}";
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
