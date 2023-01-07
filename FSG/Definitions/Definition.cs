namespace FSG.Definitions
{
    public enum DefinitionType
    {
        AgentAction,
        Building,
        EconomicCategory,
        Land,
        Resource,
        Scorer,
        Task
    }

    public interface IDefinition
    {
        public DefinitionType Type { get; }

        public string Name { get; }
    }
}