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
            // Validate input directory
            if (string.IsNullOrWhiteSpace(directory))
            {
                throw new ArgumentException("The directory path cannot be null or empty.", nameof(directory));
            }

            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException($"Directory not found: {directory}");
            }

            // Validate output file path
            if (string.IsNullOrWhiteSpace(outputZipFilePath))
            {
                throw new ArgumentException("The output zip file path cannot be null or empty.", nameof(outputZipFilePath));
            }

            try
            {
                // Perform compression
                ZipFile.CreateFromDirectory(directory, outputZipFilePath, CompressionLevel.Optimal, includeBaseDirectory: true);
                Console.WriteLine($"Successfully compressed folder '{directory}' to '{outputZipFilePath}'.");
            }
            catch (Exception ex)
            {
                // Log error or rethrow with additional context
                throw new InvalidOperationException("An error occurred while compressing the folder.", ex);
            }
        }

        public static void ExtractZipFile(string zipFilePath, string outputFolderPath)
        {
            // Validate input zip file path
            if (string.IsNullOrWhiteSpace(zipFilePath))
            {
                throw new ArgumentException("The zip file path cannot be null or empty.", nameof(zipFilePath));
            }

            if (!File.Exists(zipFilePath))
            {
                throw new FileNotFoundException($"The specified zip file does not exist: {zipFilePath}");
            }

            // Validate output folder path
            if (string.IsNullOrWhiteSpace(outputFolderPath))
            {
                throw new ArgumentException("The output folder path cannot be null or empty.", nameof(outputFolderPath));
            }

            try
            {
                // Extract the contents of the zip file
                ZipFile.ExtractToDirectory(zipFilePath, outputFolderPath, overwriteFiles: true);
                Console.WriteLine($"Successfully extracted '{zipFilePath}' to '{outputFolderPath}'.");
            }
            catch (Exception ex)
            {
                // Add context to any exception that occurs and rethrow
                throw new InvalidOperationException($"An error occurred while extracting the zip file '{zipFilePath}' to '{outputFolderPath}'.", ex);
            }
        }
    }
}
