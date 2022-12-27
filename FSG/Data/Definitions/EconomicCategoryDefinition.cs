namespace FSG.Data
{
    public struct EconomicCategoryDefinition : IDefinition
    {
        public DefinitionType Type { get => DefinitionType.EconomicCategory; }

        public string Name { get; init; }

        public string Parent { get; init; }
    }
}