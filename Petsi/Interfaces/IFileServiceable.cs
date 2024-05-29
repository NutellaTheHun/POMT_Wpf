namespace Petsi.Interfaces
{
    public interface IFileServiceable
    {
        /// <summary>
        /// IFileServicable method
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="targetObject"></param>
        public void DataListToFile<T>(string fileName, List<T> dataObject);
        /// <summary>
        /// IFileServicable method
        /// </summary>
        /// <param name="fileName"></param>
        public List<T> BuildDataListFile<T>(string fileName);
    }
}
