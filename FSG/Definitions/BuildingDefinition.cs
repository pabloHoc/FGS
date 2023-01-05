using System.Collections.Generic;
using FSG.Conditions;
using FSG.Entities;
using FSG.Scopes;

namespace FSG.Definitions
{
    public class BuildingDefinition : IDefinition
    {
        public DefinitionType Type { get => DefinitionType.Building; }

        public string Name { get; init; }

        public int BaseBuildTime { get; init; }

        public EconomyUnit Resources { get; init; }

        public FSG.Conditions.Conditions Conditions { get; init; }
    }
}