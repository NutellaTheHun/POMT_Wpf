using LibGit2Sharp;

namespace Backup.Service
{
    public class GithubService
    {
        private static readonly string configDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\petsiDir\\BackupService\\";
        private static readonly string zipFilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\petsiDir\\BackupService\\fileService.zip";
        private static readonly string enryptedFilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\petsiDir\\BackupService\\fileService.enc";
        private static readonly string decryptedFilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\petsiDir\\BackupService\\decryptfileService.zip";
        private static readonly string sourceDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\petsiDir\\fileService";
        private static readonly string configFp = configDir + "BackupServiceConfig.txt";

        private const string LocalRepoPath = "LocalRepoPath";
        private const string AuthorName = "AuthorName";
        private const string AuthorAddress = "AuthorAddress";
        private const string RemotePath = "RemotePath";
        private const string Username = "Username";
        private const string Password = "Password";
        private const string Key = "Key";

        Dictionary<string, string> variables;

        private bool IsReady;

        public GithubService() 
        {
            IsReady = false;
            Initialize();
        }

        private void Initialize()
        {
            if (!Directory.Exists(configDir)) { Directory.CreateDirectory(configDir); }
            if (!File.Exists(configFp)) { File.Create(configFp); }

            variables = File.ReadAllLines(configFp)
              .Select(l => l.Split(new[] { '=' }))
              .ToDictionary(s => s[0].Trim(), s => s[1].Trim());

            ValidateVariables();
        }

        private void ValidateVariables()
        {
            if(variables == null || variables.Count == 0)
            {
                IsReady = false;
                return;
            }
            bool variableCheck = true;
            CheckVariable(LocalRepoPath, ref variableCheck);
            CheckVariable(AuthorName, ref variableCheck);
            CheckVariable(AuthorAddress, ref variableCheck);
            CheckVariable(RemotePath, ref variableCheck);
            CheckVariable(Username, ref variableCheck);
            CheckVariable(Password, ref variableCheck);
            CheckVariable(Key, ref variableCheck);
            IsReady = variableCheck;
        }

        private void CheckVariable(string var, ref bool varCheck)
        {
            if (!variables.ContainsKey(var))
            {
                varCheck = false;
            }
        }

        public bool PushBackup()
        {
            if (!IsReady) { return false; }
            /*
            //---
            //string repoPath = @"path/to/your/local/repo"; // Path to your local Git repository
            //string jsonFilePath = Path.Combine(repoPath, "data.json"); // Path to your JSON file

            // Step 1: Create or update the JSON file
            //var data = new { Timestamp = DateTime.UtcNow, Message = "Backup data" };
            //File.WriteAllText(jsonFilePath, System.Text.Json.JsonSerializer.Serialize(data));
            */

            //public static void CompressFolder(string directory, string outputZipFilePath)
            CompressionHelper.CompressFolder(sourceDir, zipFilePath);
            //public static void EncryptFile(string inputFilePath, string key, string outputFilePath)
            EncryptionHelper.EncryptFile(zipFilePath, variables[Key], enryptedFilePath);
            // Step 2: Initialize or open the repository
            if (!Repository.IsValid(variables[LocalRepoPath]))
            {
                //Console.WriteLine("Initializing new repository...");
                Repository.Init(variables[LocalRepoPath]);
            }

            using var repo = new Repository(variables[LocalRepoPath]);

            // Step 3: Stage the JSON file
            Commands.Stage(repo, enryptedFilePath);

            // Step 4: Commit changes
            Signature author = new Signature(variables[AuthorName], variables[AuthorAddress], DateTime.UtcNow);
            Signature committer = author;
            repo.Commit("Backup file", author, committer);

            // Step 5: Push changes to GitHub
            var remote = repo.Network.Remotes["origin"];
            if (remote == null)
            {
                // Add your GitHub remote URL
                repo.Network.Remotes.Add("origin", variables[RemotePath]);
            }

            // Credentials for pushing to GitHub
            var pushOptions = new PushOptions
            {
                CredentialsProvider = (_, _, _) =>
                    new UsernamePasswordCredentials
                    {
                        Username = variables[Username],
                        Password = variables[Password] // Use a personal access token instead of your password
                    }
            };

            //Console.WriteLine("Pushing to GitHub...");
            repo.Network.Push(repo.Branches["main"], pushOptions); // Replace "main" with your branch name

            //---

            return true;
        }
    }
}
