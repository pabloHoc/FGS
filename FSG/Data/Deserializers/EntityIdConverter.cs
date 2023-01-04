using System;
using FSG.Common;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Text.Json;
using System.Text.Json.Serialization;
using FSG.Entities;

namespace FSG.Data
{
    public class EntityIdConverter<T> : JsonConverter<EntityId<T>>
    {
        public override EntityId<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var instance = (EntityId<T>)Activator.CreateInstance(typeToConvert, reader.GetString());
            return instance;
        }

        const string ValuePropertyName = nameof(EntityId<object>.Value);

        public override void Write(Utf8JsonWriter writer, EntityId<T> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.Value, value.Value.GetType(), options);
        }
    }

    internal class EntityIdConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType)
                return false;

            return typeToConvert.GetGenericTypeDefinition() == typeof(EntityId<>);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            Type wrappedType = typeToConvert.GetGenericArguments()[0];

            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(EntityIdConverter<>).MakeGenericType(wrappedType));

            return converter;
        }
    }

}

