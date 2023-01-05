namespace FSG.Definitions
{
    public class ResourceDefinition : IDefinition
    {
        public DefinitionType Type { get => DefinitionType.Resource; }

        public string Name { get; init; }
    }
}