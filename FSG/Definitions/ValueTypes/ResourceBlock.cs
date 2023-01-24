using System.Collections.Generic;

namespace FSG.Definitions
{
    public enum EconomicType
    {
        Resources,
        Upkeep,
        Production
    }

    // TODO: this is not longer a struct, check where to put it

    public class ResourceBlock
    {
        public string Category { get; init; }

        // Cost for buildings, resources for empires/regions
        public Dictionary<string, decimal> Resources { get; init; } = new();

        public Dictionary<string, decimal> Upkeep { get; init; } = new();

        public Dictionary<string, decimal> Production { get; init; } = new();
    }
}