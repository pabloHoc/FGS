// TODO: Add conditions

namespace FSG.Data
{
    public struct BuildingDefinition : IDefinition
    {
        public DefinitionType Type { get => DefinitionType.Building; }

        public string Name { get; init; }

        public int BaseBuildTime { get; init; }

        public EconomyUnit Resources { get; init; }
    }
}