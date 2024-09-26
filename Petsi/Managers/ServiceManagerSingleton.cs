using Petsi.Interfaces;
using Petsi.Services;

namespace Petsi.Managers
{
    /// <summary>
    /// Singleton collection of services. Classes that inherit from ServiceBase can register itself on instantiaion to this class to be referenced throughout the application.
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

        /// <summary>
        /// Used for testing purposes only. (Required for properly reseting contexts accross xunit tests.
        /// </summary>
        public static void Reset()
        {
            instance.services.Clear();
        }
    }
}
