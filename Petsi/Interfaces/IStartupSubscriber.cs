
namespace Petsi.Interfaces
{
    public interface IStartupSubscriber
    {
        public void Update(List<(string fileName, string filePath)> FileList);
    }
}
