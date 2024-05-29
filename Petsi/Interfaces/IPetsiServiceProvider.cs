using Petsi.Services;

namespace Petsi.Interfaces
{
    public interface IPetsiServiceProvider
    {
        public ServiceBase GetService(string name);
    }
}
