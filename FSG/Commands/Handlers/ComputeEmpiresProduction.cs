using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;
using FSG.Entities.Queries;

namespace FSG.Commands.Handlers
{
    // TODO: We should separate upkeep from production

    public class ComputeEmpiresProduction : CommandHandler<Commands.ComputeEmpiresProduction>
    {
        public ComputeEmpiresProduction(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ComputeEmpiresProduction command)
        {
            var empires = _serviceProvider.GlobalState.Entities.GetAll<Empire>();

            foreach (var empire in empires)
            {
                ClearProductionAndUpkeep(empire);
                ComputeProduction(empire);
                ComputeUpkeep(empire);
            }
        }

        private void ClearProductionAndUpkeep(Empire empire)
        {
            foreach (var resource in empire.Resources.Production)
            {
                empire.Resources.Production[resource.Key] = 0;
                empire.Resources.Upkeep[resource.Key] = 0;
            }
        }

        private void ComputeProduction(Empire empire)
        {
            foreach (var region in empire.Regions)
            {
                ComputeRegionProduction(region, empire);
            }
        }

        private void ComputeUpkeep(Empire empire)
        {
            foreach (var region in empire.Regions)
            {
                foreach (var pop in region.Pops)
                {
                    ComputePopUpkeep(pop, empire);
                }
            }
        }

        private void ComputeRegionProduction(Region region, Empire empire)
        {
            var empireModifiers = _serviceProvider.Services.ModifierService
                .GetModifiersFor(empire);
            var regionModifiers = _serviceProvider.Services.ModifierService
                .GetModifiersFor(region);

            foreach (var land in region.Lands)
            {
                //ComputeLandProduction(land, empire);

                foreach (var building in land.Buildings)
                {
                    ComputeBuildingProduction(building, region, empire, empireModifiers, regionModifiers);
                }
            }

            foreach (var building in region.Capital.Buildings)
            {
                ComputeBuildingProduction(building, region, empire, empireModifiers, regionModifiers);
            }

            foreach (var pop in region.Pops)
            {
                ComputePopProduction(pop, empire, empireModifiers, regionModifiers);
            }
        }

        private void ComputePopUpkeep(Pop pop, Empire empire)
        {
            var strata = _serviceProvider.Definitions.Get<SocialStructureDefinition>(empire.SocialStructure)
                .Stratas.Find(strata => strata.Name == pop.Strata);

            foreach (var resource in strata.Resources.Upkeep)
            {
                if (empire.Resources.Upkeep.ContainsKey(resource.Key))
                {
                    empire.Resources.Upkeep[resource.Key] += resource.Value * pop.Size;
                }
            }
        }

        private void ComputePopProduction(
            Pop pop,
            Empire empire,
            List<Modifier> empireModifiers,
            List<Modifier> regionModifiers
        )
        {
            var strata = _serviceProvider.Definitions.Get<SocialStructureDefinition>(empire.SocialStructure)
                .Stratas.Find(strata => strata.Name == pop.Strata);
            var economicCategoryDefinition = _serviceProvider.Definitions
                .Get<EconomicCategoryDefinition>(strata.Resources.Category);

            foreach (var resource in strata.Resources.Production)
            {
                if (empire.Resources.Production.ContainsKey(resource.Key))
                {
                    var totalPopProduction =
                        _serviceProvider.Services.EconomicCategoryService.Compute(
                            economicCategoryDefinition,
                            EconomicType.Production,
                            resource.Key,
                            resource.Value,
                            empireModifiers,
                            regionModifiers
                        );

                    empire.Resources.Production[resource.Key] += totalPopProduction * pop.Size;
                }
            }
        }

        private void ComputeLandProduction(Land land, Empire empire)
        {
            var landDefinition = _serviceProvider.Definitions.Get<LandDefinition>(land.Name);

            foreach (var resource in landDefinition.Resources.Production)
            {
                if (empire.Resources.Production.ContainsKey(resource.Key))
                {
                    empire.Resources.Production[resource.Key] += resource.Value;
                }
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

                if (empire.Resources.Production.ContainsKey(resource.Key))
                {
                    empire.Resources.Production[resource.Key] += totalBuildingResourceProduction;
                }
            }
        }
    }
}

