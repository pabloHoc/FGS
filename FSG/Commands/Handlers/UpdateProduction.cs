using System;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;
using FSG.Entities.Queries;

namespace FSG.Commands.Handlers
{
    public class UpdateProduction : CommandHandler<Commands.UpdateProduction>
    {
        public UpdateProduction(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.UpdateProduction command)
        {
            var empires = _serviceProvider.GlobalState.Entities.GetAll<Empire>();

            foreach (var empire in empires)
            {
                ClearProduction(empire);
                ComputeProduction(empire);
            }
        }

        private void ClearProduction(Empire empire)
        {
            foreach (var resource in empire.Production)
            {
                empire.Production[resource.Key] = 0;
            }
        }

        private void ComputeProduction(Empire empire)
        {
            var empireRegions = _serviceProvider.GlobalState.Entities
                .Query(new GetEmpireRegions(empire.Id));

            foreach (var region in empireRegions)
            {
                ComputeRegionProduction(region, empire);
            }
        }

        private void ComputeRegionProduction(Region region, Empire empire)
        {
            var regionLands = _serviceProvider.GlobalState.Entities
                .Query(new GetRegionLands(region.Id));

            foreach (var land in regionLands)
            {
                ComputeLandProduction(land, empire);

                foreach (var building in land.Buildings)
                {
                    ComputeBuildingProduction(building, region, empire);
                }
            }
        }

        private void ComputeLandProduction(Land land, Empire empire)
        {
            var landDefinition = _serviceProvider.Definitions.Get<LandDefinition>(land.Name);

            foreach (var resource in landDefinition.Resources.Production)
            {
                empire.Production[resource.Key] += resource.Value;
            }
        }

        private void ComputeBuildingProduction(string building, Region region, Empire empire)
        {
            var buildingDefinition = _serviceProvider.Definitions.Get<BuildingDefinition>(building);
            var economicCategoryDefinition = _serviceProvider.Definitions
                .Get<EconomicCategoryDefinition>(buildingDefinition.Resources.Category);

            foreach (var resource in buildingDefinition.Resources.Production)
            {
                var totalBuildingResourceProduction = economicCategoryDefinition
                    .Compute(EconomicType.Production, resource.Key, resource.Value, empire.Modifiers, region.Modifiers);

                empire.Production[resource.Key] += totalBuildingResourceProduction;
            }
        }
    }
}

