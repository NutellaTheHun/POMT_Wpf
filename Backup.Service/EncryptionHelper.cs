using System.Security.Cryptography;
using System.Text;
using SystemLogging.Service;

namespace Backup.Service
{
    public class EncryptionHelper
    {
        private const int KeySize = 32; // 256-bit AES key size
        private const int IvSize = 16; // AES block size for IV

        /// <summary>
        /// Encrypts a file using AES-256 encryption.
        /// </summary>
        /// <param name="inputFilePath">Path to the input file to be encrypted.</param>
        /// <param name="key">Encryption key (32 bytes / 256 bits).</param>
        /// <param name="outputFilePath">Path to the output encrypted file.</param>
        public static void EncryptFile(string inputFilePath, string key, string outputFilePath)
        {
            ValidateFilePath(inputFilePath, nameof(inputFilePath));
            ValidateFilePath(outputFilePath, nameof(outputFilePath));
            byte[] keyBytes = ValidateKey(key);

            using Aes aes = Aes.Create();
            aes.Key = keyBytes;
            aes.GenerateIV(); // Generate a new IV for each encryption

            try
            {
                using var fileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
                fileStream.Write(aes.IV, 0, aes.IV.Length); // Prepend IV to the file

                using var encryptor = aes.CreateEncryptor();
                using var cryptoStream = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write);
                using var inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read);

                inputFileStream.CopyTo(cryptoStream);
            }
            catch (Exception ex)
            {
                Logger.LogError($"An error occurred during file encryption: {ex.Message}", "BackupService.Encryption");
                throw new IOException("An error occurred during file encryption.", ex);
            }
        }

        /// <summary>
        /// Decrypts an AES-256 encrypted file.
        /// </summary>
        /// <param name="inputFilePath">Path to the input encrypted file.</param>
        /// <param name="key">Decryption key (32 bytes / 256 bits).</param>
        /// <param name="outputFilePath">Path to the output decrypted file.</param>
        public static void DecryptFile(string inputFilePath, string key, string outputFilePath)
        {
            ValidateFilePath(inputFilePath, nameof(inputFilePath));
            ValidateFilePath(outputFilePath, nameof(outputFilePath));
            byte[] keyBytes = ValidateKey(key);

            using var fileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read);

            // Read IV from the beginning of the file
            byte[] iv = new byte[IvSize];
            if (fileStream.Read(iv, 0, iv.Length) != IvSize)
            {
                Logger.LogError("The input file is invalid or corrupted (missing IV).", "BackupService.Encryption");
                throw new InvalidDataException("The input file is invalid or corrupted (missing IV).");
            }

            using Aes aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = iv;

            try
            {
                using var decryptor = aes.CreateDecryptor();
                using var cryptoStream = new CryptoStream(fileStream, decryptor, CryptoStreamMode.Read);
                using var outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);

                cryptoStream.CopyTo(outputFileStream);
            }
            catch (Exception ex)
            {
                Logger.LogError($"An error occurred during file decryption: {ex.Message}", "BackupService.Encryption");
                throw new IOException("An error occurred during file decryption.", ex);
            }
        }

        /// <summary>
        /// Validates the encryption/decryption key.
        /// </summary>
        private static byte[] ValidateKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                Logger.LogError("Key cannot be null or empty.", "BackupService.Encryption");
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));
            }

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            if (keyBytes.Length != KeySize)
            {
                Logger.LogError($"Key must be {KeySize} bytes long (256-bit).", "BackupService.Encryption");
                throw new ArgumentException($"Key must be {KeySize} bytes long (256-bit).");
            }

            return keyBytes;
        }

        /// <summary>
        /// Validates file paths for null or invalid values.
        /// </summary>
        private static void ValidateFilePath(string filePath, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Logger.LogError($"File path cannot be null or empty. {parameterName}", "BackupService.Encryption");
                throw new ArgumentException("File path cannot be null or empty.", parameterName);
            }
        }
    }

}
