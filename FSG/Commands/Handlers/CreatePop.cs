using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Entities;
using FSG.Entities.Queries;

namespace FSG.Commands.Handlers
{
    public class CreatePop : CommandHandler<Commands.CreatePop>
    {
        public CreatePop(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.CreatePop command)
        {
            var pops = _serviceProvider.GlobalState.Entities.Query(new GetRegionPops(command.RegionId));
            var strataPop = pops.Find(pop => pop.Strata == command.Strata);

            if (strataPop == null)
            {
                _serviceProvider.GlobalState.Entities.Add(new Pop
                {
                    Id = new EntityId<Pop>(),
                    RegionId = command.RegionId,
                    Strata = command.Strata,
                    Size = command.Size
                });
            }
        }
    }
}

