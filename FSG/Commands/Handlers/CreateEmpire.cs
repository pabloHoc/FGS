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
            var config = _serviceProvider.Definitions.Get<SetupConfigDefinition>("Default");
            var resources = _serviceProvider.Definitions.GetAll<ResourceDefinition>()
                .FindAll(resource => resource.Scope == Scopes.Scope.Empire);
            var resourceBlock = new ResourceBlock();

            foreach (var resource in resources)
            {
                resourceBlock.Resources.Add(resource.Name, config.StartingResources[resource.Name]);
                resourceBlock.Production.Add(resource.Name, 0);
                resourceBlock.Upkeep.Add(resource.Name, 0);
            }

            var empire = new Empire
            {
                Id = new EntityId<Empire>(),
                Name = command.EmpireName,
                Resources = resourceBlock,
                SocialStructure = "Feudal",
                Regions = new List<Region>(),
                Agents = new List<Agent>(),
                Armies = new List<Army>(),
                Modifiers = new List<Modifier>()
            };

            _serviceProvider.GlobalState.World.Empires.Add(empire);
            _serviceProvider.GlobalState.World.LastAddedEntityId = empire.Id;
        }
    }
}

