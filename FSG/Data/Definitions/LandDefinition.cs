namespace FSG.Data
{
    public struct LandDefinition : IDefinition
    {
        public DefinitionType Type { get => DefinitionType.Land; }

        public string Name { get; init; }

        public EconomyUnit Resources { get; init; }
    }
}