using System.Security.Cryptography;
using System.Text;

namespace Common.Helper.Encrypt;

public class Md5Encrypt
{
  static string Key { get; set; } = "A!9HXhi%XjjYY4YP2@Tob009X";
  private static readonly Random Random = new Random();

  public static string RandomString()
  {
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    return new string(Enumerable.Repeat(chars, 5)
      .Select(s => s[Random.Next(s.Length)]).ToArray());
  }

  public static string Encrypt(string text)
  {
    using var md5 = MD5.Create();
    using var tdes = TripleDES.Create();
    tdes.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(Key));
    tdes.Mode = CipherMode.ECB;
    tdes.Padding = PaddingMode.PKCS7;

    using var transform = tdes.CreateEncryptor();
    byte[] textBytes = Encoding.UTF8.GetBytes(text);
    byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
    return Convert.ToBase64String(bytes, 0, bytes.Length);
  }

  public static string Decrypt(string cipher)
  {
    using var md5 = MD5.Create();
    using var tdes = TripleDES.Create();
    tdes.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(Key));
    tdes.Mode = CipherMode.ECB;
    tdes.Padding = PaddingMode.PKCS7;

    using var transform = tdes.CreateDecryptor();
    byte[] cipherBytes = Convert.FromBase64String(cipher);
    byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
    return Encoding.UTF8.GetString(bytes);
  }
}