using System;

namespace FSG.Entities
{
    public interface IEntityId { }

    public interface IEntityId<T> : IEntityId { }

    public struct EntityId<T> : IEntityId<T>
    {
        public readonly string Value { get; init; }

        public EntityId()
        {
            this.Value = Guid.NewGuid().ToString();
        }

        public EntityId(string value)
        {
            this.Value = value;
        }

        public static explicit operator string(EntityId<T> id)
        {
            return id.Value;
        }

        public static explicit operator EntityId<T>(string value)
        {
            return new EntityId<T>(value);
        }
    }
}