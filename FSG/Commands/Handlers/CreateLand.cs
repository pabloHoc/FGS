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
            var land = new Land
            {
                Id = new EntityId<Land>(),
                Name = command.LandName,
                RegionId = command.RegionId,
                Buildings = new List<string>(),
            };
            _serviceProvider.GlobalState.Entities.Add(land);
            _serviceProvider.GlobalState.Entities.Get(command.RegionId).Lands.Add(land);
        }
    }
}

