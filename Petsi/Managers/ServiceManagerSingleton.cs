using Petsi.Interfaces;
using Petsi.Services;

namespace Petsi.Managers
{
    /// <summary>
    /// Orignally implemented for CommandFrame functionality, managers became main way of refrencing services and models.
    /// </summary>
    public class ServiceManagerSingleton : IPetsiServiceProvider, IServiceManagerRegistry
    {
        private static ServiceManagerSingleton instance;
        private List<ServiceBase> services;
        private ServiceManagerSingleton() { services = new List<ServiceBase>(); }

        public static ServiceManagerSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new ServiceManagerSingleton();
            }
            return instance;
        }
        public ServiceBase GetService(string name)
        {
            return services.Find(x => x.GetServiceName() == name);
        }
        public void AddAvailableService(ServiceBase service) { services.Add(service); }

        public void Register(ServiceBase service)
        {
            services.Add(service);
        }

        public void Deregister(ServiceBase service)
        {
            services.Remove(service);
        }

        public void Notify()
        {
            throw new NotImplementedException();
        }
    }
}
