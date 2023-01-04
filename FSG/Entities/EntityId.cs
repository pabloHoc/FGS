using System;
using FSG.Common;

namespace FSG.Entities
{
    public interface IEntityId { }

    public struct EntityId<T> : IEntityId
    {
        public string Value { get; init; }

        public EntityId()
        {
            Value = Guid.NewGuid().ToString();
        }

        public EntityId(string value)
        {
            Value = value;
        }
    }
}