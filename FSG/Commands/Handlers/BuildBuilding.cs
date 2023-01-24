using System;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class BuildBuilding : CommandHandler<Commands.BuildBuilding>
    {
        public BuildBuilding(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.BuildBuilding command)
        {
            var buildingDefinition = _serviceProvider.Definitions.Get<BuildingDefinition>(command.BuildingName);

            switch (command.BuildingType)
            {
                case BuildingType.LandBuilding:
                {
                    var land = _serviceProvider.GlobalState.Entities.Get(command.LandId);
                    land.Buildings.Add(command.BuildingName);
                    break;
                }
                case BuildingType.CapitalBuilding:
                {
                    var region = _serviceProvider.GlobalState.Entities.Get(command.RegionId);
                    region.Capital.Buildings.Add(command.BuildingName);
                    break;
                }
                case BuildingType.District:
                {
                    var region = _serviceProvider.GlobalState.Entities.Get(command.RegionId);
                    region.Capital.Districts.Add(new District {
                        Name = command.BuildingName,
                    });
                    break;
                }
            }

            if (buildingDefinition.OnBuilt != null)
            {
                var region = _serviceProvider.GlobalState.Entities.Get(command.RegionId);
                _serviceProvider.ActionProcessor.Process(buildingDefinition.OnBuilt, region, region.Id);
            }
        }
    }
}

