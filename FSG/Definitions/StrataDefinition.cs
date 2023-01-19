using System;
namespace FSG.Definitions
{
	public class StrataDefinition : IDefinition
	{
        public DefinitionType Type => DefinitionType.Strata;

        public string Name { get; init; }

        public ResourceBlock Resources { get; init; }
    }
}

