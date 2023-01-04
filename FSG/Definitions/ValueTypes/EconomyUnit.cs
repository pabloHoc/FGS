using System.Collections.Generic;

namespace FSG.Definitions
{
    public enum EconomicType
    {
        Cost,
        Upkeep,
        Production
    }

    public struct EconomyUnit
    {
        public string Category { get; init; }

        public Dictionary<string, int> Cost { get; init; }

        public Dictionary<string, int> Upkeep { get; init; }

        public Dictionary<string, int> Production { get; init; }
    }
}