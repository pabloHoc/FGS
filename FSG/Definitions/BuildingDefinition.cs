using System.Collections.Generic;
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

    public interface IBuildingDefinition
    {
        public string Name { get; init; }

        public BuildingType BuildingType { get; }

        public int BaseBuildTime { get; init; }

        public EconomyUnit Resources { get; init; }

        public FSG.Conditions.Conditions Conditions { get; init; }
    }

    public class BuildingDefinition : IDefinition, IBuildingDefinition
    {
        public DefinitionType Type => DefinitionType.Building;

        public BuildingType BuildingType { get; init; }

        public string Name { get; init; }

        public int BaseBuildTime { get; init; }

        public EconomyUnit Resources { get; init; }

        public FSG.Conditions.Conditions Conditions { get; init; }
    }
}