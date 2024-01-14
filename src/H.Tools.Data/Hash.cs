using System;
using System.Security.Cryptography;
using System.Text;

namespace H.Tools.Data;

public static class Hash
{
    public static string ToX2(this byte[] data)
    {
        StringBuilder sb = new();
        for (int i = 0; i < data.Length; i++)
        {
            sb.Append(data[i].ToString("x2"));
        }
        return sb.ToString();
    }

    public static byte[] Sha256(byte[] data)
    {
        return SHA256.Create().ComputeHash(data);
    }

    public static string Sha256(string str)
    {
        return Sha256(Encoding.UTF8.GetBytes(str)).ToX2();
    }

    public static string Sha256ToBase64(byte[] data)
    {
        return Convert.ToBase64String(Sha256(data));
    }

    public static byte[] Md5(byte[] data)
    {
        return MD5.Create().ComputeHash(data);
    }

    public static string Md5(string str)
    {
        return Md5(Encoding.UTF8.GetBytes(str)).ToX2();
    }

    public static string Md5ToBase64(byte[] data)
    {
        return Convert.ToBase64String(Md5(data));
    }
}
