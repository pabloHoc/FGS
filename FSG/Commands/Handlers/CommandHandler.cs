using FSG.Core;

namespace FSG.Commands.Handlers
{
    public interface IBaseCommandHandler {
    }

    public interface ICommandHandler<TCommand> : IBaseCommandHandler where TCommand : ICommand
    {
        public void Handle(TCommand command);
    }

    public abstract class CommandHandler<TCommand> : IBaseCommandHandler where TCommand : ICommand
    {
        protected ServiceProvider _serviceProvider { get; }

        protected CommandHandler(ServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public abstract void Handle(TCommand command);
    }
}