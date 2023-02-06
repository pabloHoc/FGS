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
            var region = _serviceProvider.GlobalState.World.Regions
                .Find(region => region.Id == command.RegionId);

            switch (command.BuildingType)
            {
                case BuildingType.LandBuilding:
                {
                    var land = region.Lands.Find(land => land.Id == command.LandId);
                    land.Buildings.Add(command.BuildingName);
                    break;
                }
                case BuildingType.CapitalBuilding:
                {
                    region.Capital.Buildings.Add(command.BuildingName);
                    break;
                }
                case BuildingType.District:
                {
                    region.Capital.Districts.Add(new District {
                        Name = command.BuildingName,
                    });
                    break;
                }
            }

            region.ComputeProduction = true;

            if (buildingDefinition.OnBuilt != null)
            {
                _serviceProvider.ActionProcessor.Process(buildingDefinition.OnBuilt, region, region.Id);
            }
        }
    }
}

