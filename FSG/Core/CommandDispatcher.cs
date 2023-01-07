using System;
using FSG.Commands;
using FSG.Commands.Handlers;

namespace FSG.Core
{
    public class CommandDispatcher
    {
        public event EventHandler<ICommand> OnCommandDispatched;
        public event EventHandler<ICommand> OnCommandProcessed;

        private HandlerMap _handlerRepository { get; }

        public CommandDispatcher(ServiceProvider serviceProvider)
        {
            this._handlerRepository = new HandlerMap(serviceProvider);
        }

        public void Dispatch<T>(T command) where T : ICommand
        {
            OnCommandDispatched.Invoke(this, command);
            this._handlerRepository.Handle(command);
            OnCommandProcessed.Invoke(this, command);
        }
    }
}