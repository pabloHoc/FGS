using System.Collections.Generic;
using FSG.Commands;
using FSG.Conditions;
using FSG.Entities;
using FSG.Scopes;

namespace FSG.Definitions
{
    public enum BuildingType
    {
        LandBuilding,
        CapitalBuilding,
        District
    }

    public class BuildingDefinition : IDefinition
    {
        public DefinitionType DefinitionType => DefinitionType.Building;

        public BuildingType BuildingType { get; init; }

        public string Name { get; init; }

        public string Category { get; init; }

        public int BaseBuildTime { get; init; }

        public ResourceBlock Resources { get; init; }

        public FSG.Conditions.Conditions Conditions { get; init; }

        public Actions OnBuilt { get; init; }
    }
}