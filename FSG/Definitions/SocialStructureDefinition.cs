﻿using System;
using System.Collections.Generic;

namespace FSG.Definitions
{
	public class Strata
	{
        public string Name { get; init; }

        public ResourceBlock Resources { get; init; }
    }

    public class SocialStructureDefinition : IDefinition
    {
        public DefinitionType Type => DefinitionType.SocialStructure;

        public string Name { get; init; }

        public List<Strata> Stratas { get; init; }
    }
}

