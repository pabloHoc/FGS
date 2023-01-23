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
            var resources = _serviceProvider.Definitions.GetAll<ResourceDefinition>()
                .FindAll(resource => resource.Scope == Scopes.Scope.Empire);
            var resourceBlock = new ResourceBlock();

            foreach (var resource in resources)
            {
                // TODO: make a config for initial resources
                resourceBlock.Resources.Add(resource.Name, 100);
                resourceBlock.Production.Add(resource.Name, 0);
                resourceBlock.Upkeep.Add(resource.Name, 0);
            }

            _serviceProvider.GlobalState.Entities.Add(new Empire
            {
                Id = new EntityId<Empire>(),
                Name = command.EmpireName,
                Resources = resourceBlock,
            });
        }
    }
}

