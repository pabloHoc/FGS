using System;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;
using FSG.Entities.Queries;

namespace FSG.Commands.Handlers
{
	public class ProcessResourceLevels : CommandHandler<Commands.ProcessResourceLevels>
	{
		public ProcessResourceLevels(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ProcessResourceLevels command)
        {
            var regions = _serviceProvider.GlobalState.Entities.Query(new GetRegionsWithEmpire());
            var resourceDefinitions = _serviceProvider.Definitions.GetAll<ResourceDefinition>();

            // TODO: process empires
            foreach (var region in regions)
            {
                foreach (var resource in region.Resources.Resources)
                {
                    var resourceDefinition = resourceDefinitions
                        .Find(definition => definition.Name == resource.Key);

                    if (resource.Value > 20)
                    {
                        // TODO: remove region.Id
                        _serviceProvider.ActionProcessor.Process(resourceDefinition.Actions, region, region.Id);
                    }
                }
            }
        }
    }
}

