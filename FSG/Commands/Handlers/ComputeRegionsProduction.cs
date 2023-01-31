using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;
using FSG.Entities.Queries;

namespace FSG.Commands.Handlers
{
    // TODO: We should separate upkeep from production

    public class ComputeRegionsProduction : CommandHandler<Commands.ComputeRegionsProduction>
    {
        public ComputeRegionsProduction(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ComputeRegionsProduction command)
        {
            var regions = _serviceProvider.GlobalState.Entities.GetAll<Region>();

            foreach (var region in regions)
            {
                ClearProductionAndUpkeep(region);
                ComputeProduction(region);
                ComputeUpkeep(region);
            }
        }

        private void ClearProductionAndUpkeep(Region region)
        {
            foreach (var resource in region.Resources.Production)
            {
                region.Resources.Production[resource.Key] = 0;
                region.Resources.Upkeep[resource.Key] = 0;
            }
        }

        private void ComputeProduction(Region region)
        {
            var regionModifiers = _serviceProvider.Services.ModifierService
                .GetModifiersFor(region);

            foreach (var land in region.Lands)
            {
                //ComputeLandProduction(land, region);

                foreach (var building in land.Buildings)
                {
                    ComputeBuildingProduction(building, region, regionModifiers);
                }
            }

            foreach (var building in region.Capital.Buildings)
            {
                ComputeBuildingProduction(building, region, regionModifiers);
            }

            foreach (var pop in region.Pops)
            {
                ComputePopProduction(pop, region);
            }
        }

        private void ComputeUpkeep(Region region)
        {
            foreach (var pop in region.Pops)
            {
                ComputePopUpkeep(pop, region);
            }
        }

        private void ComputePopUpkeep(Pop pop, Region region)
        {
            var empire = _serviceProvider.GlobalState.Entities.Get(region.EmpireId);
            var strata = _serviceProvider.Definitions.Get<SocialStructureDefinition>(empire.SocialStructure)
                .Stratas.Find(strata => strata.Name == pop.Strata);

            foreach (var resource in strata.Resources.Upkeep)
            {
                if (region.Resources.Upkeep.ContainsKey(resource.Key))
                {
                    region.Resources.Upkeep[resource.Key] += resource.Value * pop.Size;
                }
            }
        }

        private void ComputePopProduction(Pop pop, Region region)
        {
            var empire = _serviceProvider.GlobalState.Entities.Get(region.EmpireId);
            var strata = _serviceProvider.Definitions.Get<SocialStructureDefinition>(empire.SocialStructure)
                .Stratas.Find(strata => strata.Name == pop.Strata);

            foreach (var resource in strata.Resources.Production)
            {
                if (region.Resources.Production.ContainsKey(resource.Key))
                {
                    region.Resources.Production[resource.Key] += resource.Value * pop.Size;
                }
            }
        }

        private void ComputeLandProduction(Land land, Region region)
        {
            var landDefinition = _serviceProvider.Definitions.Get<LandDefinition>(land.Name);

            foreach (var resource in landDefinition.Resources.Production)
            {
                if (region.Resources.Production.ContainsKey(resource.Key))
                {
                    region.Resources.Production[resource.Key] += resource.Value;
                }
            }
        }

        private void ComputeBuildingProduction(
            string building,
            Region region,
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
                        new List<Modifier>(),
                        regionModifiers
                    );

                if (region.Resources.Production.ContainsKey(resource.Key))
                {
                    region.Resources.Production[resource.Key] += totalBuildingResourceProduction;
                }
            }
        }
    }
}

