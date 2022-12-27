using System;
using System.Collections.Generic;
using FSG.Entities;

namespace FSG.Core
{
    public class EntityRepository
    {
        private interface IEntityDictionary { }

        private class EntityDictionary<TEntity> : Dictionary<IEntityId<TEntity>, TEntity>, IEntityDictionary
            where TEntity : IBaseEntity
        { }

        private readonly Dictionary<Type, IEntityDictionary> _entities = new Dictionary<Type, IEntityDictionary>()
        {
            { typeof(Player), new EntityDictionary<Player>() },
            { typeof(Empire), new EntityDictionary<Empire>() },
            { typeof(Region), new EntityDictionary<Region>() },
            { typeof(Land), new EntityDictionary<Land>() },
            { typeof(BuildingQueueItem), new EntityDictionary<BuildingQueueItem>() },
            { typeof(Agent), new EntityDictionary<Agent>() },
            { typeof(Army), new EntityDictionary<Army>() },
            { typeof(ActionQueueItem), new EntityDictionary<ActionQueueItem>() },
            { typeof(Spell), new EntityDictionary<Spell>() },
            { typeof(Modifier), new EntityDictionary<Modifier>() }
        };

        public void Add<T>(IEntity<T> entity) where T : IEntity<T>
        {
            ((EntityDictionary<T>)_entities[typeof(T)]).Add(entity.Id, (T)entity);
        }

        public T Get<T>(IEntityId<T> entityId) where T : IEntity<T>
        {
            return ((EntityDictionary<T>)_entities[typeof(T)])[entityId];
        }

        public List<T> GetAll<T>() where T : IEntity<T>
        {
            return new List<T>(((EntityDictionary<T>)_entities[typeof(T)]).Values);
        }

        public void Remove<T>(IEntityId<T> entityId) where T : IEntity<T>
        {
            ((EntityDictionary<T>)_entities[typeof(T)]).Remove(entityId);
        }
    }
}