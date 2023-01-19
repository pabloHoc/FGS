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
        public Dictionary<string, int> Resources { get; init; } = new Dictionary<string, int>();

        public Dictionary<string, int> Upkeep { get; init; } = new Dictionary<string, int>();

        public Dictionary<string, int> Production { get; init; } = new Dictionary<string, int>();
    }
}