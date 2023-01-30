using System;
using System.Collections.Generic;

namespace FSG.Definitions
{
    public class GrowthFactors
    {
        public string BuildingCategory { get; init; }
    }

	public class Strata
	{
        public string Name { get; init; }

        public ResourceBlock Resources { get; init; }

        public GrowthFactors GrowthFactors { get; init; }
    }

    public class SocialStructureDefinition : IDefinition
    {
        public DefinitionType DefinitionType => DefinitionType.SocialStructure;

        public string Name { get; init; }

        public List<Strata> Stratas { get; init; }
    }
}

