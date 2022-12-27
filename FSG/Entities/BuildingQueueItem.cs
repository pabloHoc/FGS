namespace FSG.Entities
{
    public class BuildingQueueItem : IEntityWithTurns<BuildingQueueItem>
    {
        public string Type { get => "BUILDING_QUEUE_ITEM"; }

        public EntityId<BuildingQueueItem> Id { get; init; }

        public EntityId<Land> LandId { get; }

        public int RemainingTurns { get; set; }
    }
}