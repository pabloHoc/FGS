using FSG.Definitions;

namespace FSG.Entities
{
    public class BuildingQueueItem : ITemporary
    {
        public string Name { get; init; }

        public BuildingType BuildingType { get; init; }

        public EntityId<Land> LandId { get; init; }

        public EntityId<Region> RegionId { get; init; }

        public int RemainingTurns { get; set; }
    }
}