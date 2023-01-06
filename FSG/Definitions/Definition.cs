namespace FSG.Definitions
{
    public enum DefinitionType
    {
        AgentAction,
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