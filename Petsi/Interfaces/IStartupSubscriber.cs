
namespace Petsi.Interfaces
{
    public interface IStartupSubscriber
    {
        /// <summary>
        /// IStartupSubscriber Method
        /// </summary>
        /// <param name="FileList"></param>
        public void LoadStartupFiles(List<(string fileName, string filePath)> FileList);
    }
}
