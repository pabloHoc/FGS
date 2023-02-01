using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
	public class ProcessPopsGrowth : CommandHandler<Commands.ProcessPopsGrowth>
	{
        public ProcessPopsGrowth(ServiceProvider serviceProvider) : base(serviceProvider) { }

        private List<string> GetRegionBuidings(Region region)
        {
            var buildings = new List<string>();

            foreach (var land in region.Lands)
            {
                buildings.AddRange(land.Buildings);
            }

            buildings.AddRange(region.Capital.Buildings);

            return buildings;
        }

        public override void Handle(Commands.ProcessPopsGrowth command)
        {
            var regions = _serviceProvider.GlobalState.World.Regions.FindAll(region => region.Empire != null);
            var buildingDefinitions = _serviceProvider.Definitions.GetAll<BuildingDefinition>();
            var config = _serviceProvider.Definitions.Get<SetupConfigDefinition>("Default");

            foreach (var region in regions)
            {
                var socialStructure = _serviceProvider.Definitions.Get<SocialStructureDefinition>(region.Empire.SocialStructure);
                var buildings = GetRegionBuidings(region);

                foreach (var pop in region.Pops)
                {
                    var buildingCategory = socialStructure.Stratas
                        .Find(strata => strata.Name == pop.Strata).GrowthFactors.BuildingCategory;

                    foreach (var building in buildings)
                    {
                        var buildingDefinition = buildingDefinitions.Find(definition => definition.Name == building);

                        if (buildingDefinition.Category == buildingCategory)
                        {
                            pop.GrowthPoints++;
                        }

                        if (pop.GrowthPoints > 0 && pop.GrowthPoints % config.GrowthPointLevels[pop.Strata] == 0)
                        {
                            pop.Size++;
                        }
                    }
                }
            }
        }
    }
}

