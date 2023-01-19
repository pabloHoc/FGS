namespace FSG.Definitions
{
    public class LandDefinition : IDefinition
    {
        public DefinitionType Type => DefinitionType.Land;

        public string Name { get; init; }

        public ResourceBlock Resources { get; init; }
    }
}