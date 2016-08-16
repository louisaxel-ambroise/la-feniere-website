namespace Gite.Cqrs.Events
{
    public interface IEventHandler
    {
    }

    public interface IEventHandler<in T> : IEventHandler
    {
        void Handle(T @event);
    }
}