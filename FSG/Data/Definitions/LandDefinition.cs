namespace FSG.Data
{
    public struct LandDefinition : IDefinition
    {
        public string Name { get; init; }

        public EconomyUnit Resources { get; init; }
    }
}