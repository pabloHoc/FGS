using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class SetOwnerEmpire<T> : CommandHandler<Commands.SetOwnerEmpire<T>> where T : IEntity<T>, IOwneable
    {
        public SetOwnerEmpire(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.SetOwnerEmpire<T> command)
        {
            var entity = _serviceProvider.GlobalState.Entities.Get(command.EntityId);
            entity.EmpireId = command.EmpireId;
        }
    }
}