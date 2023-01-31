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
            var region = _serviceProvider.GlobalState.Entities.Get(command.RegionId);
            var strataPop = region.Pops.Find(pop => pop.Strata == command.Strata);

            if (strataPop == null)
            {
                var pop = new Pop
                {
                    Id = new EntityId<Pop>(),
                    RegionId = command.RegionId,
                    Strata = command.Strata,
                    Size = command.Size
                };
                _serviceProvider.GlobalState.Entities.Add(pop);
                region.Pops.Add(pop);
            }
        }
    }
}

