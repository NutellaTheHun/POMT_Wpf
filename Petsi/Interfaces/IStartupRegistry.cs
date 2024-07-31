namespace Petsi.Interfaces
{
    public interface IStartupRegistry
    {
        public void Register(IStartupSubscriber subscriber);
        public void Deregister(IStartupSubscriber subscriber);
        public void Notify();
    }
}
