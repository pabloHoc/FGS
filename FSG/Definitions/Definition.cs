namespace FSG.Definitions
{
    public enum DefinitionType
    {
        AgentAction,
        Building,
        District,
        EconomicCategory,
        Land,
        Resource,
        Scorer,
        SocialStructure,
        Spell,
        Task
    }

    public interface IDefinition
    {
        public DefinitionType Type { get; }

        public string Name { get; }
    }
}