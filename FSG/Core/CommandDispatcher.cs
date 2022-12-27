using FSG.Commands;
using FSG.Commands.Handlers;

namespace FSG.Core
{
    public class CommandDispatcher
    {
        private HandlerRepository _handlerRepository { get; }

        public CommandDispatcher(ServiceProvider serviceProvider)
        {
            this._handlerRepository = new HandlerRepository(serviceProvider);
        }

        public void Dispatch<T>(T command) where T : ICommand
        {
            this._handlerRepository.Get<T>().Handle(command);
        }
    }
}