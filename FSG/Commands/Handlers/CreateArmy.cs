using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreateArmy : CommandHandler<Commands.CreateArmy>
    {
        public CreateArmy(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.CreateArmy command)
        {
            _serviceProvider.GlobalState.Entities.Add(new Army
            {
                Id = new EntityId<Army>(),
                Size = command.Size,
                Attack = command.Attack,
                Defense = command.Defense,
                EmpireId = command.EmpireId,
                RegionId = command.RegionId,
            });
        }
    }
}

