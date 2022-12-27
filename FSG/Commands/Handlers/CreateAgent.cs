using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreateLand : CommandHandler<Commands.CreateLand>
    {
        public CreateLand(ServiceProvider serviceProvider) : base(serviceProvider) { }

        override public void Handle(Commands.CreateLand command)
        {
            _serviceProvider.GlobalState.Entities.Add(new Land
            {
                Id = new EntityId<Land>(),
                Name = command.Name,
                RegionId = command.RegionId,
                Buildings = new List<string>(),
                Modifiers = new List<Modifier>()
            });
        }
    }
}

