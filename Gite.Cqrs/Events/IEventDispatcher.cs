namespace Gite.Cqrs.Events
{
    public interface IEventDispatcher
    {
        void Dispatch<T>(T @event) where T : IEvent;
    }
}