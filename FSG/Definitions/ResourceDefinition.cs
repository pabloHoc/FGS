namespace FSG.Definitions
{
    public class ResourceDefinition : IDefinition
    {
        public DefinitionType Type => DefinitionType.Resource;

        public string Name { get; init; }
    }
}