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
            _serviceProvider.GlobalState.Entities.Add(new Land
            {
                Id = new EntityId<Land>(),
                Name = command.LandName,
                RegionId = command.RegionId,
                Buildings = new List<string>(),
                Modifiers = new List<Modifier>(),
                BuildingQueue = new Queue<BuildingQueueItem>()
            });
        }
    }
}

