using System;

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

    public interface ITemporaryEntity: IBaseEntity
    {
        public int RemainingTurns { get; set; }
    }

    public interface ILocatableEntity : IBaseEntity
    {
        public EntityId<Region> RegionId { get; set; }
    }

    public interface IOwneableEntity : IBaseEntity
    {
        public Nullable<EntityId<Empire>> EmpireId { get; set; }
    }

    public interface INameableEntity : IBaseEntity
    {
        public string Name { get; init; }
    }
}