using System;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
	public class ProcessResourceLevels : CommandHandler<Commands.ProcessResourceLevels>
	{
		public ProcessResourceLevels(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ProcessResourceLevels command)
        {
            var regions = _serviceProvider.GlobalState.World.Regions.FindAll(region => region.Empire != null);
            var resourceDefinitions = _serviceProvider.Definitions.GetAll<ResourceDefinition>();

            // TODO: process empires
            foreach (var region in regions)
            {
                foreach (var resource in region.Resources.Resources)
                {
                    var resourceDefinition = resourceDefinitions
                        .Find(definition => definition.Name == resource.Key);

                    if (resourceDefinition.Actions != null && resource.Value > 20)
                    {
                        // TODO: remove region.Id
                        _serviceProvider.ActionProcessor.Process(resourceDefinition.Actions, region, region.Id);
                    }
                }
            }
        }
    }
}

