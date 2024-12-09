using System.IO.Compression;

namespace Backup.Service
{
    public class CompressionHelper
    {
        //string zipFilePath = @"path/to/output/archive.zip";
        //string outputFolder = @"path/to/output/folder";
        //string decryptedZipFilePath = @"path/to/output/decrypted_archive.zip";
        public static void CompressFolder(string directory, string outputZipFilePath)
        {
            if (Directory.Exists(directory))
            {
                ZipFile.CreateFromDirectory(directory, outputZipFilePath, CompressionLevel.Optimal, true);
            }
            else
            {
                throw new DirectoryNotFoundException($"Directory not found: {directory}");
            }
        }

        public static void ExtractZipFile(string zipFilePath, string outputFolderPath)
        {
            if (File.Exists(zipFilePath))
            {
                ZipFile.ExtractToDirectory(zipFilePath, outputFolderPath, true);
            }
            else
            {
                throw new FileNotFoundException($"File not found: {zipFilePath}");
            }
        }
    }
}
