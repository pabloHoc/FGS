using System;

namespace FSG.Definitions
{
    // TODO: this should be generic (EntityAction) and have a entity
    public class AgentActionDefinition : IDefinition
    {
        public DefinitionType Type => DefinitionType.AgentAction;

        public string Name { get; init; }

        public int BaseExecutionTime { get; init; }

        public FSG.Conditions.Conditions Conditions { get; init; }
    }
}

