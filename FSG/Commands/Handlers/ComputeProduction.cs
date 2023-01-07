using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;
using FSG.Entities.Queries;

namespace FSG.Commands.Handlers
{
    public class ComputeProduction : CommandHandler<Commands.ComputeProduction>
    {
        public ComputeProduction(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ComputeProduction command)
        {
            var empires = _serviceProvider.GlobalState.Entities.GetAll<Empire>();

            foreach (var empire in empires)
            {
                ClearEmpireProduction(empire);
                ComputeEmpireProduction(empire);
            }
        }

        private void ClearEmpireProduction(Empire empire)
        {
            foreach (var resource in empire.Production)
            {
                empire.Production[resource.Key] = 0;
            }
        }

        private void ComputeEmpireProduction(Empire empire)
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

            var empireModifiers = _serviceProvider.Services.ModifierService
                .GetModifiersFor(empire);
            var regionModifiers = _serviceProvider.Services.ModifierService
                .GetModifiersFor(region);

            foreach (var land in regionLands)
            {
                ComputeLandProduction(land, empire);

                foreach (var building in land.Buildings)
                {
                    ComputeBuildingProduction(building, region, empire, empireModifiers, regionModifiers);
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

        private void ComputeBuildingProduction(
            string building,
            Region region,
            Empire empire,
            List<Modifier> empireModifiers,
            List<Modifier> regionModifiers
        )
        {
            var buildingDefinition = _serviceProvider.Definitions.Get<BuildingDefinition>(building);
            var economicCategoryDefinition = _serviceProvider.Definitions
                .Get<EconomicCategoryDefinition>(buildingDefinition.Resources.Category);

            foreach (var resource in buildingDefinition.Resources.Production)
            {
                var totalBuildingResourceProduction =
                    _serviceProvider.Services.EconomicCategoryService.Compute(
                        economicCategoryDefinition,
                        EconomicType.Production,
                        resource.Key,
                        resource.Value,
                        empireModifiers,
                        regionModifiers
                    );

                empire.Production[resource.Key] += totalBuildingResourceProduction;
            }
        }
    }
}

