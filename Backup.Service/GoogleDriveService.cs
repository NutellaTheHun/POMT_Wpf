using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace Backup.Service
{
    public class GoogleDriveService
    {
        private static readonly string serviceAccountKey = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "petsiDir\\BackupService\\pomtbackup-3bbf60ee72bd.JSON");
        private static readonly string configFp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "petsiDir\\BackupService\\BackupServiceConfig.txt");
        private static readonly string uploadDateFp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "petsiDir\\BackupService\\uploaddate.txt");

        private static readonly string sourceDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "petsiDir\\fileService");
        private static readonly string zipFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "petsiDir\\BackupService\\fileService.zip");
        private static readonly string encryptedFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "petsiDir\\BackupService\\fileService.enc");
        
        private const string ENCKEY = "enckey";
        private const string BACKUPID = "backupid";

        private const int FILE_AMOUNT_LIMIT = 5;

        public static void HandleBackupUpload()
        {
            var variables = GetVariables();
            string previousUploadDate = File.ReadAllText(uploadDateFp);
            string today = DateTime.Today.ToShortDateString();

            if (previousUploadDate != today)
            {
                try
                {
                    var driveService = AuthenticateServiceAccount(serviceAccountKey);
                    HandleBackupSize(driveService);
                    if (UploadBackup(variables, driveService))
                    {
                        File.WriteAllText(uploadDateFp, today);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
                
                Cleanup();
            }
        }

        private static bool UploadBackup(Dictionary<string,string> variables, DriveService driveService)
        {
            if (!IsReady()) 
            { 
                return false;
            }

            CompressionHelper.CompressFolder(sourceDir, zipFilePath);
            EncryptionHelper.EncryptFile(zipFilePath, variables[ENCKEY], encryptedFilePath);

            try
            {
                FileToDrive(driveService, encryptedFilePath, "petsiDir.enc", "application/octet-stream", variables[BACKUPID]);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error uploading file: {e.Message}");
                return false;
            }

            return true;
        }

        private static void HandleBackupSize(DriveService service)
        {
            var filesRequest = service.Files.List();
                filesRequest.OrderBy = "createdTime";
            var filesList = filesRequest.Execute();
            if(filesList.Files.Count >= FILE_AMOUNT_LIMIT)
            {
                var oldestFile = filesList.Files[0]; // The oldest file, first in list
                var deleteRequest = service.Files.Delete(oldestFile.Id);
                try
                {
                    deleteRequest.Execute();
                    //Console.WriteLine($"Deleted oldest file: {oldestFile.Name} (ID: {oldestFile.Id})");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error deleting file: {e.Message}");
                }
            }
        }

        private static DriveService AuthenticateServiceAccount(string serviceAccountKeyFile)
        {
            GoogleCredential credential;
            using (var stream = new FileStream(serviceAccountKeyFile, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(DriveService.ScopeConstants.DriveFile);
            }

            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "File Uploader"
            });
        }

        private static void FileToDrive(DriveService service, string filePath, string fileName, string mimeType, string folderPath)
        {
            var fileMetaData = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName,
                Parents = new[] { folderPath }
            };

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var request = service.Files.Create(fileMetaData, fileStream, mimeType);
                request.Fields = "id, name";
                try
                {
                    request.Upload();
                    var file = request.ResponseBody;
                    //Console.WriteLine($"Uploaded file: {file.Name} (ID: {file.Id})");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error uploading file: {e.Message}");
                    throw;
                }
            }
        }

        private static bool IsReady()
        {
            if (!File.Exists(configFp))
            {
                return false;
            }

            Dictionary<string, string> variables = GetVariables();
            if(!variables.ContainsKey(ENCKEY) || !variables.ContainsKey(BACKUPID))
            {
                return false;
            }

            if (!Directory.Exists(sourceDir))
            {
                return false;
            }
            return true;
        }

        private static Dictionary<string, string> GetVariables() 
        {
            return File.ReadAllLines(configFp)
              .Select(l => l.Split(new[] { '=' }))
              .ToDictionary(s => s[0].Trim(), s => s[1].Trim());
        }

        private static void Cleanup()
        {
            if (File.Exists(zipFilePath)) { File.Delete(zipFilePath);  }
            if (File.Exists(encryptedFilePath)) { File.Delete(encryptedFilePath); }
        }
    }
}
