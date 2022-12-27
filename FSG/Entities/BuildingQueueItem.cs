// TODO: THIS SHOULDN'T BE A ENTITY NOR HAVE ID, SAME FOR MODIFIER AND SPELL

namespace FSG.Entities
{
    public class BuildingQueueItem : IEntity<BuildingQueueItem>, IEntityWithTurns
    {
        public EntityType Type { get; } = EntityType.BuildingQueueItem;

        public EntityId<BuildingQueueItem> Id { get; init; }

        public string Name { get; init; }

        public EntityId<Land> LandId { get; }

        public int RemainingTurns { get; set; }
    }
}