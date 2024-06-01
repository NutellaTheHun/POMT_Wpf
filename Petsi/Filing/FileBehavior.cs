using Petsi.Interfaces;

namespace Petsi.Filing
{
    public class FileBehavior : IFileServiceable
    {
        protected string directoryName;

        public FileBehavior(string componentName)
        {
            directoryName = componentName;
        }
        public void ListFileDirectory()
        {
            string[] result = FileService.GetFileDirectoryList(directoryName);
            if (result.Length == 0)
            {
                Console.WriteLine("No files exist.");
            }
            else
            {
                for (int i = 0; i < result.Length; i++)
                {
                    Console.WriteLine("[" + i + "] " + Path.GetFileName(result[i]));
                }
            } 
        }
        public List<T> BuildDataListFile<T>(string fileName)
        {
            return FileService.FileToDataList<T>(directoryName, fileName);
        }
        public void DataListToFile<T>(string fileName, List<T> dataList)
        {
            FileService.Save(directoryName, fileName, dataList);
        }
    }
}
