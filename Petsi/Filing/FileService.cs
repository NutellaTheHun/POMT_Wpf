using Newtonsoft.Json;
using Petsi.Services;
using Petsi.Utils;

namespace Petsi.Filing
{
    public static class FileService
    {
        private static string ServicePath()
        {
            return PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_FILESERVICE_PATH);
        }
        public static void DataObjectToFile<T>(string directory, string fileName, List<T> target)
        {
            ValidateDirectory(directory);
            try
            {
                File.WriteAllText(ServicePath() + directory + "/" + fileName, JsonConvert.SerializeObject(target));
            }
            catch (Exception ex) { ErrorService.RaiseExceptionHandlerError(ex.Message, "FileService, DataObjectToFile"); }
        }
        private static void ValidateDirectory(string dirFileName)
        {
            string x = ServicePath();
            if (!Directory.Exists(ServicePath() + dirFileName))
            {
                Directory.CreateDirectory(ServicePath() + "/" + dirFileName);
            }
        }
        public static string[] GetFileDirectoryList(string directoryName)
        {
            if (!Directory.Exists(ServicePath() + "/" + directoryName))
            {
                Directory.CreateDirectory(ServicePath() + "/" + directoryName);
            }
            return Directory.GetFiles(ServicePath() + "/" + directoryName);
        }

        
        public static void Save<T>(string directory, string fileName, T target)
        {
            ValidateDirectory(directory);
            try
            {
                File.WriteAllText(ServicePath() + "/" + directory + "/" + fileName, JsonConvert.SerializeObject(target));
            }
            catch (Exception ex) { ErrorService.RaiseExceptionHandlerError(ex.Message, "FileService, Save()"); }
        }
        

        public static List<T> FileToDataList<T>(string directory, string fileName)
        {
            ValidateDirectory(directory);
            string input;
            try
            {
                input = File.ReadAllText(ServicePath() + "/" + directory + "/" + fileName);
            }
            catch(Exception ex)
            {
                ErrorService.RaiseExceptionHandlerError(ex.Message, "FileService, FileToDataList");
                return null;
            }
            
            return JsonConvert.DeserializeObject<List<T>>(input);
        }

    }
}