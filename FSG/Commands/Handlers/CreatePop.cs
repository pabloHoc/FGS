using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreatePop : CommandHandler<Commands.CreatePop>
    {
        public CreatePop(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.CreatePop command)
        {
            _serviceProvider.GlobalState.Entities.Add(new Pop
            {
                Id = new EntityId<Pop>(),
                RegionId = command.RegionId,
                Strata = command.Strata
            });
        }
    }
}

