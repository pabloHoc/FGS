using System;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class UpdateRegionsResources : CommandHandler<Commands.UpdateRegionsResources>
    {
        public UpdateRegionsResources(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.UpdateRegionsResources command)
        {
            var regions = _serviceProvider.GlobalState.World.Regions.FindAll(region => region.Empire != null);

            foreach (var region in regions)
            {
                foreach (var entry in region.Resources.Resources)
                {
                    var hadDeficit = region.Resources.Resources[entry.Key] < 0;
                    
                    var resourceDefinition = _serviceProvider.Definitions.Get<ResourceDefinition>(entry.Key);

                    if (!resourceDefinition.IsStockpiled)
                    {
                        region.Resources.Resources[entry.Key] = 0;
                    }
                    
                    region.Resources.Resources[entry.Key] += region.Resources.Production[entry.Key];
                    region.Resources.Resources[entry.Key] -= region.Resources.Upkeep[entry.Key];

                    if (resourceDefinition.OnDeficit != null)
                    {
                        var hasDeficit = region.Resources.Resources[entry.Key] < 0;
                        
                        if (!hadDeficit && hasDeficit)
                        {
                            _serviceProvider.ActionProcessor.Process(resourceDefinition.OnDeficit, region, region.Id);
                        } else if (hadDeficit && !hasDeficit)
                        {
                            // TODO: Revert modifier   
                        }
                    }
                }
            }
        }
    }
}

