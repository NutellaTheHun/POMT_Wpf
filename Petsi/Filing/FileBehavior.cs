using Newtonsoft.Json;
using Petsi.Interfaces;
using Petsi.Services;

namespace Petsi.Filing
{
    public class FileBehavior : IFileServiceable
    {
        protected string directoryName;

        public FileBehavior(string componentName)
        {
            directoryName = componentName;
        }
        
        public List<T> BuildDataListFile<T>(string fileName)
        {
            //if(directoryName == "ERROR") { return null; }
            return FileService.FileToDataList<T>(directoryName, fileName);
        }
        public void DataListToFile<T>(string fileName, List<T> dataList)
        {
            //if (directoryName == "ERROR") { return; }
            FileService.Save(directoryName, fileName, dataList);
        }

        /// <summary>
        /// The directoryname is used as a full directory path, should be renamed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        public void DataListToPureFilePath<T>(string fileName, List<T> dataList)
        {
            //File.WriteAllText(directoryName, JsonConvert.SerializeObject(dataList));
            //if (directoryName == "ERROR") { return; }
            try
            {
                File.WriteAllText(directoryName + "\\" + fileName, JsonConvert.SerializeObject(dataList));
            }
            catch (Exception ex) { ErrorService.RaiseExceptionHandlerError(ex.Message, "FileBehavior, DataListToPureFilePath"); }
        }

        public string GetDirectoryName() { return directoryName; }
    }
}
