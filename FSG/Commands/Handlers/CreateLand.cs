using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreateLand : CommandHandler<Commands.CreateLand>
    {
        public CreateLand(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.CreateLand command)
        {
            var region = _serviceProvider.GlobalState.World.Regions
                .Find(region => region.Id == command.RegionId);

            var land = new Land
            {
                Id = new EntityId<Land>(),
                Name = command.LandName,
                Region = region,
                Buildings = new List<string>(),
            };

            region.Lands.Add(land);
            _serviceProvider.GlobalState.World.Lands.Add(land);
        }
    }
}

