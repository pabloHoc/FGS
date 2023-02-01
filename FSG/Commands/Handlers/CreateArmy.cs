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
            var empire = _serviceProvider.GlobalState.World.Empires.Find(empire => empire.Id == command.EmpireId);
            var region = _serviceProvider.GlobalState.World.Regions.Find(region => region.Id == command.RegionId);

            var army = new Army
            {
                Id = new EntityId<Army>(),
                Size = command.Size,
                Attack = command.Attack,
                Defense = command.Defense,
                Empire = empire,
                Region = region,
            };

            empire.Armies.Add(army);
        }
    }
}

