namespace FSG.Definitions
{
    public struct ResourceDefinition : IDefinition
    {
        public DefinitionType Type { get => DefinitionType.Resource; }

        public string Name { get; init; }
    }
}