using System;
using FSG.Common;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FSG.Data
{
    public class ValueObjectConverter<T> : JsonConverter<ValueObject<T>>
    {
        public override ValueObject<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Here we assume that every subclass of ValueObject<T> has a constructor with a single argument, of type T.
            var instance = (ValueObject<T>)Activator.CreateInstance(typeToConvert, reader.GetString());
            return instance;
        }

        const string ValuePropertyName = nameof(ValueObject<object>.Value);

        public override void Write(Utf8JsonWriter writer, ValueObject<T> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.Value, value.Value.GetType(), options);
        }
    }

    internal class ValueObjectConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType)
                return false;

            return typeToConvert.GetGenericTypeDefinition() == typeof(ValueObject<>);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            Type wrappedType = typeToConvert.GetGenericArguments()[0];

            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(ValueObjectConverter<>).MakeGenericType(wrappedType));

            return converter;
        }
    }

}

