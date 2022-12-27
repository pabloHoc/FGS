namespace FSG.Commands
{
    public interface IBaseCommandHandler { }

    public interface ICommandHandler<TCommand> : IBaseCommandHandler where TCommand : ICommand
    {
        public void Handle(TCommand command);
    }
}