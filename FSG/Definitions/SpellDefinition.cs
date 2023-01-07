using System;
using FSG.Commands;

namespace FSG.Definitions
{
    public class SpellDefinition : IDefinition
    {
        public DefinitionType Type => DefinitionType.Building;

        public string Name { get; init; }

        public int BaseExecutionTime { get; init; }

        public int Duration { get; init; }

        public FSG.Conditions.Conditions Conditions { get; init; }

        public Actions Actions { get; init; }
    }
}

