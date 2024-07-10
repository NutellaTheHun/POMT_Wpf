using Petsi.Models;

namespace Petsi.Services
{
    public abstract class ServiceBase
    {
        protected string serviceName;
        public virtual string GetServiceName(){ return serviceName; }
        protected virtual void SetServiceName(string name) { serviceName = name; }

        public abstract void Update(ModelBase model);
    }
}
