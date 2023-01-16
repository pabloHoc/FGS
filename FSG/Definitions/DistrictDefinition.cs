using System;
using FSG.Entities;

namespace FSG.Definitions
{
	public class DistrictDefinition : IDefinition, IBuildingDefinition
    {
        public DefinitionType Type => DefinitionType.District;

        public BuildingType BuildingType => BuildingType.District;

        public string Name { get; init; }

        public int BaseBuildTime { get; init; }

        public EconomyUnit Resources { get; init; }

        public FSG.Conditions.Conditions Conditions { get; init; }
    }
}

