using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class SetLocation<T> : CommandHandler<Commands.SetLocation<T>> where T : IEntity<T>, IEntityWithLocation
    {
        public SetLocation(ServiceProvider serviceProvider) : base(serviceProvider) { }

        override public void Handle(Commands.SetLocation<T> command)
        {
            var entity = _serviceProvider.GlobalState.Entities.Get(command.EntityId);
            entity.RegionId = command.RegionId;
        }
    }
}