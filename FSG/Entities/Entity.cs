using System;
using System.Collections.Generic;

namespace FSG.Entities
{
    public enum EntityType
    {
        Agent,
        Army,
        Empire,
        Land,
        Player,
        Region,
        Spell,
        Modifier
    }

    public interface IBaseEntity
    {
        public EntityType Type { get; }
    }

    public interface IEntity<T> : IBaseEntity where T : IEntity<T>
    {

        public EntityId<T> Id { get; }
    }

    public interface ITemporary
    {
        public int RemainingTurns { get; set; }
    }

    public interface ILocatable
    {
        public EntityId<Region> RegionId { get; set; }
    }

    public interface IOwneable
    {
        public Nullable<EntityId<Empire>> EmpireId { get; set; }
    }

    public interface INameable
    {
        public string Name { get; init; }
    }

    // TODO: change name
    public interface IActor
    {
        public Queue<ActionQueueItem> Actions { get; init; }
    }
}