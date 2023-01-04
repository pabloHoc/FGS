using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreateEmpire : CommandHandler<Commands.CreateEmpire>
    {
        public CreateEmpire(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.CreateEmpire command)
        {
            var resources = _serviceProvider.Definitions.GetAll<ResourceDefinition>();

            var stored = new Dictionary<string, int>();
            var production = new Dictionary<string, int>();

            foreach(var resource in resources)
            {
                stored.Add(resource.Name, 0);
                production.Add(resource.Name, 0);
            }

            _serviceProvider.GlobalState.Entities.Add(new Empire
            {
                Id = new EntityId<Empire>(),
                Name = command.Name,
                Resources = stored,
                Production = production,
                Modifiers = new List<Entities.Modifier>()
            });
        }
    }
}

