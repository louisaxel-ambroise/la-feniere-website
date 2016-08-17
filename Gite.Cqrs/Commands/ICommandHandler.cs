namespace Gite.Cqrs.Commands
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<in T> : ICommandHandler
    {
        void Handle(T command);
    }
}