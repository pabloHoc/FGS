using System;
using System.Collections.Generic;
using FSG.Entities;

namespace FSG.Core
{
    public interface IQuery<T> where T : IEntity<T>
    {
        public Predicate<T> GetPredicate();
    }

    public class EntityRepository
    {
        public class EntityDictionaryMap
        {
            public Dictionary<string, Player> Player { get; init; } = new Dictionary<string, Player>();
            public Dictionary<string, Empire> Empire { get; init; } = new Dictionary<string, Empire>();
            public Dictionary<string, Region> Region { get; init; } = new Dictionary<string, Region>();
            public Dictionary<string, Land> Land { get; init; } = new Dictionary<string, Land>();
            public Dictionary<string, Agent> Agent { get; init; } = new Dictionary<string, Agent>();
            public Dictionary<string, Army> Army { get; init; } = new Dictionary<string, Army>();
            public Dictionary<string, Spell> Spell { get; init; } = new Dictionary<string, Spell>();
            public Dictionary<string, Modifier> Modifier { get; init; } = new Dictionary<string, Modifier>();

            public Dictionary<string, T> Get<T>() where T : IBaseEntity
            {
                // TODO: Review reflection perfomance, use switch instead
                return (Dictionary<string, T>)this.GetType().GetProperty(typeof(T).Name).GetValue(this);
            }
        }

        public readonly EntityDictionaryMap _entities = new EntityDictionaryMap();

        private IEntityId _lastAddedEntityId;

        public void Add<T>(IEntity<T> entity) where T : IEntity<T>
        {
            _entities.Get<T>().Add(entity.Id, (T)entity);
            _lastAddedEntityId = entity.Id;
        }

        public T Get<T>(EntityId<T> entityId) where T : IEntity<T>
        {
            return _entities.Get<T>()[entityId];
        }

        public List<T> Query<T>(IQuery<T> query) where T : IEntity<T>
        {
            return GetAll<T>().FindAll(query.GetPredicate());
        }

        public List<T> GetAll<T>() where T : IEntity<T>
        {
            return new List<T>(_entities.Get<T>().Values);
        }

        public void Remove<T>(IEntity<T> entity) where T : IEntity<T>
        {
            _entities.Get<T>().Remove(entity.Id);
        }

        // WARNING: Use inmmediately after dispatch create entity command only,
        // otherwise cast could be wrong
        public EntityId<T> GetLastAddedEntityId<T>() where T : IEntity<T>
        {
            return (EntityId<T>)_lastAddedEntityId;
        }
    }
}