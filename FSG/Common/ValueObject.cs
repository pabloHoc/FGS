using System;
namespace FSG.Common
{
    public interface IValueObject { }

	public abstract class ValueObject<T> : IValueObject
	{
        public T Value { get; init; }

        public ValueObject() { }

        public ValueObject(T value) => Value = value;
    }
}

