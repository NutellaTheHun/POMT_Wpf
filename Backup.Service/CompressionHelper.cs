using System.IO.Compression;
using SystemLogging.Service;

namespace Backup.Service
{
    public class CompressionHelper
    {
        public static void CompressFolder(string directory, string outputZipFilePath)
        {
            // Validate input directory
            if (string.IsNullOrWhiteSpace(directory))
            {
                Logger.LogError("The directory path cannot be null or empty.", "BackupService.Compression");
                throw new ArgumentException("The directory path cannot be null or empty.", nameof(directory));
            }

            if (!Directory.Exists(directory))
            {
                Logger.LogError($"Directory not found: {directory}", "BackupService.Compression");
                throw new DirectoryNotFoundException($"Directory not found: {directory}");
            }

            // Validate output file path
            if (string.IsNullOrWhiteSpace(outputZipFilePath))
            {
                Logger.LogError("The output zip file path cannot be null or empty.", "BackupService.Compression");
                throw new ArgumentException("The output zip file path cannot be null or empty.", nameof(outputZipFilePath));
            }

            try
            {
                // Perform compression
                ZipFile.CreateFromDirectory(directory, outputZipFilePath, CompressionLevel.Optimal, includeBaseDirectory: true);
                Logger.LogStatus($"BackupService.Compression: Successfully compressed folder '{directory}' to '{outputZipFilePath}'.");
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
                Logger.LogError("The zip file path cannot be null or empty.", "BackupService.Compression");
                throw new ArgumentException("The zip file path cannot be null or empty.", nameof(zipFilePath));
            }

            if (!File.Exists(zipFilePath))
            {
                Logger.LogError($"The specified zip file does not exist: {zipFilePath}", "BackupService.Compression");
                throw new FileNotFoundException($"The specified zip file does not exist: {zipFilePath}");
            }

            // Validate output folder path
            if (string.IsNullOrWhiteSpace(outputFolderPath))
            {
                Logger.LogError("The output folder path cannot be null or empty.", "BackupService.Compression");
                throw new ArgumentException("The output folder path cannot be null or empty.", nameof(outputFolderPath));
            }

            try
            {
                // Extract the contents of the zip file
                ZipFile.ExtractToDirectory(zipFilePath, outputFolderPath, overwriteFiles: true);
                Logger.LogStatus($"Successfully extracted '{zipFilePath}' to '{outputFolderPath}'.");
            }
            catch (Exception ex)
            {
                // Add context to any exception that occurs and rethrow
                throw new InvalidOperationException($"An error occurred while extracting the zip file '{zipFilePath}' to '{outputFolderPath}'.", ex);
            }
        }
    }
}
