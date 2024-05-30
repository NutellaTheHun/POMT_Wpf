using Newtonsoft.Json;
using Petsi.Utils;

namespace Petsi.Filing
{
    public static class FileService
    {
        private static string ServicePath()
        {
            return PetsiConfig.GetInstance().GetFilepath("fileServicePath");
        }
        public static void DataObjectToFile<T>(string directory, string fileName, List<T> target)
        {
            ValidateDirectory(directory);
            File.WriteAllText(ServicePath() + directory + "/" + fileName, JsonConvert.SerializeObject(target));
        }
        private static void ValidateDirectory(string dirFileName)
        {
            if (!Directory.Exists(ServicePath() + dirFileName))
            {
                Directory.CreateDirectory(ServicePath() + dirFileName);
            }
        }
        public static string[] GetFileDirectoryList(string directoryName)
        {
            if (!Directory.Exists(ServicePath() + directoryName))
            {
                Directory.CreateDirectory(ServicePath() + directoryName);
            }
            return Directory.GetFiles(ServicePath() + directoryName);
        }
        public static void Save<T>(string directory, string fileName, T target)
        {
            ValidateDirectory(directory);
            File.WriteAllText(ServicePath() + directory + "/" + fileName, JsonConvert.SerializeObject(target));
        }
        public static List<T> FileToDataList<T>(string directory, string fileName)
        {
            ValidateDirectory(directory);
            string input;
            try
            {
                input = File.ReadAllText(ServicePath() + directory + "/" + fileName);
            }catch(FileNotFoundException ex)
            {
                return null;
            }
            
            return JsonConvert.DeserializeObject<List<T>>(input);
        }

    }
}