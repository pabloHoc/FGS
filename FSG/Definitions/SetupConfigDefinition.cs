﻿using System;
using System.Collections.Generic;

namespace FSG.Definitions
{
	public class SetupConfigDefinition : IDefinition
	{
        public DefinitionType DefinitionType => DefinitionType.SetupConfig;

        public string Name { get; init; }

        public Dictionary<string, decimal> StartingResources { get; init; }

        public Dictionary<string, int> StartingPops { get; init; }

        public Dictionary<string, int> GrowthPointLevels { get; init; }

        public int LandMaxBuildings { get; init; }
    }
}

