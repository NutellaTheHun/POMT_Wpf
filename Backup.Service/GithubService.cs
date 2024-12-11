using LibGit2Sharp;

namespace Backup.Service
{
    public class GithubService
    {
        private static readonly string configDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\petsiDir\\BackupService\\repo";
        private static readonly string zipFilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\petsiDir\\BackupService\\repo\\fileService.zip";
        private static readonly string enryptedFilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\petsiDir\\BackupService\\repo\\fileService.enc";
        private static readonly string decryptedFilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\petsiDir\\BackupService\\repo\\decryptfileService.zip";
        private static readonly string sourceDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\petsiDir\\fileService";
        private static readonly string configFp = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\petsiDir\\BackupService\\BackupServiceConfig.txt";

        private const string LocalRepoPath = "LocalRepoPath";
        private const string AuthorName = "AuthorName";
        private const string AuthorAddress = "AuthorAddress";
        private const string RemotePath = "RemotePath";
        private const string Username = "Username";
        private const string Password = "Password";
        private const string Token = "Token";
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
            CheckVariable(Token, ref variableCheck);
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

            CompressionHelper.CompressFolder(sourceDir, zipFilePath);
            EncryptionHelper.EncryptFile(zipFilePath, variables[Key], enryptedFilePath);

            // Step 2: Initialize or open the repository
            if (!Repository.IsValid(configDir))
            {
                //Console.WriteLine("Initializing new repository...");
                Repository.Init(configDir);
                
            }

            using var repo = new Repository(configDir);
            var branch = repo.Branches["main"] ?? repo.CreateBranch("main");
            Commands.Checkout(repo, branch);


            // Step 3: Stage the file
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
                remote = repo.Network.Remotes["origin"];
            }

            // Link the branch to the remote
            repo.Branches.Update(branch, b => b.Remote = remote.Name, b => b.UpstreamBranch = branch.CanonicalName);

            // Credentials for pushing to GitHub
            var pushOptions = new PushOptions
            {
                CredentialsProvider = (_, _, _) =>
                    new UsernamePasswordCredentials
                    {
                        Username = "",
                        Password = variables[Token]
                    }
            };
            

            //Console.WriteLine("Pushing to GitHub...");
            //repo.Network.Push(repo.Branches["main"], pushOptions); // Replace "main" with your branch name
            repo.Network.Push(branch, pushOptions);
            //---
            /*
             new UsernamePasswordCredentials
                    {
                        Username = "",
                        Password = variables[Token]
                    }
             */
            return true;
        }
    }
}
