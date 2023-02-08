using System;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class UpdateEmpiresResources : CommandHandler<Commands.UpdateEmpiresResources>
    {
        public UpdateEmpiresResources(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.UpdateEmpiresResources command)
        {
            var empires = _serviceProvider.GlobalState.World.Empires;

            foreach (var empire in empires)
            {
                foreach (var entry in empire.Resources.Resources)
                {
                    var hadDeficit = empire.Resources.Resources[entry.Key] < 0;

                    var resourceDefinition = _serviceProvider.Definitions.Get<ResourceDefinition>(entry.Key);

                    if (!resourceDefinition.IsStockpiled)
                    {
                        empire.Resources.Resources[entry.Key] = 0;
                    }

                    empire.Resources.Resources[entry.Key] += empire.Resources.Production[entry.Key];
                    empire.Resources.Resources[entry.Key] -= empire.Resources.Upkeep[entry.Key];
                    
                    if (resourceDefinition.OnDeficit != null)
                    {
                        var hasDeficit = empire.Resources.Resources[entry.Key] < 0;
                        
                        if (!hadDeficit && hasDeficit)
                        {
                            _serviceProvider.ActionProcessor.Process(resourceDefinition.OnDeficit, empire, empire.Id);
                        } else if (hadDeficit && !hasDeficit)
                        {
                            _serviceProvider.ActionProcessor.Revert(resourceDefinition.OnDeficit, empire, empire.Id);
                        }
                    }
                }
            }
        }
    }
}

