namespace FSG.Data
{
    public enum DefinitionType
    {
        Building,
        EconomicCategory,
        Land,
        Resource
    }

    public interface IDefinition
    {
        public DefinitionType Type { get; }

        public string Name { get; }
    }
}