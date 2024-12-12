using System.Security.Cryptography;
using System.Text;

namespace Backup.Service
{
    public class EncryptionHelper
    {
        public static void EncryptFile(string inputFilePath, string key, string outputFilePath)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            if (keyBytes.Length != 32)
                throw new ArgumentException("Key must be 32 bytes long (256-bit).");

            using Aes aes = Aes.Create();
            aes.Key = keyBytes;
            aes.GenerateIV();

            using var fileStream = new FileStream(outputFilePath, FileMode.Create);
            fileStream.Write(aes.IV, 0, aes.IV.Length); // Write IV at the start

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var cryptoStream = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write);
            using var inputFileStream = new FileStream(inputFilePath, FileMode.Open);

            inputFileStream.CopyTo(cryptoStream);
        }

        public static void DecryptFile(string inputFilePath, string key, string outputFilePath)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            if (keyBytes.Length != 32)
                throw new ArgumentException("Key must be 32 bytes long (256-bit).");

            using var fileStream = new FileStream(inputFilePath, FileMode.Open);

            // Read IV
            byte[] iv = new byte[16];
            fileStream.Read(iv, 0, iv.Length);

            using Aes aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var cryptoStream = new CryptoStream(fileStream, decryptor, CryptoStreamMode.Read);
            using var outputFileStream = new FileStream(outputFilePath, FileMode.Create);

            cryptoStream.CopyTo(outputFileStream);
        }
    }
}
