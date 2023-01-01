using System;
using FSG.Commands;
using FSG.Commands.Handlers;

namespace FSG.Core
{
    public class CommandDispatcher
    {
        public event EventHandler<ICommand> CommandDispatched;

        private HandlerRepository _handlerRepository { get; }

        public CommandDispatcher(ServiceProvider serviceProvider)
        {
            this._handlerRepository = new HandlerRepository(serviceProvider);
        }

        public void Dispatch<T>(T command) where T : ICommand
        {
            CommandDispatched.Invoke(this, command);
            this._handlerRepository.Get<T>().Handle(command);
        }
    }
}