using System;
using System.Collections.Generic;
using FSG.Entities;

namespace FSG.Definitions
{
    public class EconomicCategoryDefinition : IDefinition
    {
        public DefinitionType Type => DefinitionType.EconomicCategory;

        public string Name { get; init; }

        public string Parent { get; init; }
    }
}