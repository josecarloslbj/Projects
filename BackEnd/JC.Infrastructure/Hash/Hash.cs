using System.Security.Cryptography;
using System.Text;

namespace JC.Infrastructure.Hash;

public static class Hash
{
    /// <summary>
    /// Gera uma string baseada em uma senha e um salt
    /// </summary>
    /// <param name="value">Senha em texto puro</param>
    /// <param name="salt">Salt usado</param>
    /// <returns>Senha criptografada</returns>
    public static string HashSHA256(string value, string salt)
    {
        byte[] saltedValue = Encoding.UTF8.GetBytes(value).Concat(Encoding.UTF8.GetBytes(salt)).ToArray();
        return Convert.ToBase64String(new SHA256Managed().ComputeHash(saltedValue));
    }

    public static string HashMD5(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        var md5 = new MD5CryptoServiceProvider();
        md5.ComputeHash(Encoding.ASCII.GetBytes(text));
        var result = md5.Hash;

        var strBuilder = new StringBuilder();

        for (int i = 0; i < result.Length; i++)
        {
            strBuilder.Append(result[i].ToString("x2"));
        }

        return strBuilder.ToString();
    }

    private static string keyMD5 = "35A6A9F7-F0F5-4A75-94EF-D7637EBED808";
    public static string DecryptMD5(string cipherString)
    {
        byte[] keyArray;
        byte[] toEncryptArray = Convert.FromBase64String(cipherString);

        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(keyMD5));
        hashmd5.Clear();

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        tdes.Clear();
        return UTF8Encoding.UTF8.GetString(resultArray);
    }
    public static string EncryptMD5(string toEncrypt)
    {
        byte[] keyArray;
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(keyMD5));
        hashmd5.Clear();

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = tdes.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        tdes.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    /// <summary>
    /// Gera uma chave hash para uma string. Similar à object.GetHashCode(),
    /// porém invariante quanto à versão do runtime.
    /// </summary>
    /// <param name="str">String pegar o hash</param>
    /// <returns>string hash</returns>
    public static int DeterministicHashCode(string str)
    {
        unchecked
        {
            int hash1 = (5381 << 16) + 5381;
            int hash2 = hash1;

            for (int i = 0; i < str.Length; i += 2)
            {
                hash1 = ((hash1 << 5) + hash1) ^ str[i];
                if (i == str.Length - 1)
                    break;
                hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
            }

            return hash1 + (hash2 * 1566083941);
        }
    }
}
