using System;
using System.Collections.Generic;
using FSG.Core;

namespace FSG.Commands
{
    public class HandlerRepository
    {
        private Dictionary<Type, IBaseCommandHandler> _handlers { get; }

        public HandlerRepository(ServiceProvider serviceProvider)
        {
            this._handlers = new Dictionary<Type, IBaseCommandHandler>()
            {
                { typeof(CreatePlayer), new CreatePlayerHandler(serviceProvider) }
            };
        }

        public ICommandHandler<T> Get<T>() where T : ICommand
        {
            return (ICommandHandler<T>)this._handlers[typeof(T)];
        }
    }
}