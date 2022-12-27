namespace FSG.Entities
{
    public enum EntityType
    {
        ActionQueueItem,
        Agent,
        Army,
        BuildingQueueItem,
        Empire,
        Land,
        Modifier,
        Player,
        Region,
        Spell
    }

    public interface IBaseEntity
    {
        public EntityType Type { get; }
    }

    public interface IEntity<T> : IBaseEntity where T : IEntity<T>
    {

        public EntityId<T> Id { get; }
    }

    public interface IEntityWithTurns: IBaseEntity
    {
        public int RemainingTurns { get; set; }
    }

    public interface IEntityWithLocation : IBaseEntity
    {
        public EntityId<Region> RegionId { get; set; }
    }

    public interface IEntityWithOwner : IBaseEntity
    {
        public EntityId<Empire>? EmpireId { get; set; }
    }
}