
namespace Petsi.Interfaces
{
    public interface IOrderModelPublisher
    {
        public void Notify();
        public void Subscribe(IOrderModelSubscriber subscription);
    }
}
