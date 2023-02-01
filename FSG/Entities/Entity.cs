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
        Modifier,
        Player,
        Pop,
        Region,
        Spell,
    }

    public interface IBaseEntity
    {
        public EntityType EntityType { get; }
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
        public Region Region { get; set; }
    }

    public interface IOwneable
    {
        public Empire Empire { get; set; }
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