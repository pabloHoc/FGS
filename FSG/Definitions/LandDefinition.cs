namespace FSG.Definitions
{
    public class LandDefinition : IDefinition
    {
        public DefinitionType Type { get => DefinitionType.Land; }

        public string Name { get; init; }

        public EconomyUnit Resources { get; init; }
    }
}