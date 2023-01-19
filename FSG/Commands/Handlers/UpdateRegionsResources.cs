using System;
using FSG.Core;
using FSG.Entities;
using FSG.Entities.Queries;

namespace FSG.Commands.Handlers
{
    public class UpdateRegionsResources : CommandHandler<Commands.UpdateRegionsResources>
    {
        public UpdateRegionsResources(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.UpdateRegionsResources command)
        {
            var regions = _serviceProvider.GlobalState.Entities.Query(new GetRegionsWithEmpire());

            foreach (var region in regions)
            {
                foreach (var entry in region.Resources.Resources)
                {
                    region.Resources.Resources[entry.Key] += region.Resources.Production[entry.Key];
                    region.Resources.Resources[entry.Key] -= region.Resources.Upkeep[entry.Key];
                }
            }
        }
    }
}

