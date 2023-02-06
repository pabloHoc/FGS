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
            var region = _serviceProvider.GlobalState.World.Regions
                .Find(region => region.Id == command.RegionId);
            var strataPop = region.Pops.Find(pop => pop.Strata == command.Strata);

            if (strataPop == null)
            {
                var pop = new Pop
                {
                    Id = new EntityId<Pop>(),
                    Region = region,
                    Strata = command.Strata,
                    Size = command.Size
                };
                region.Pops.Add(pop);
                region.ComputeProduction = true;
            }
        }
    }
}

