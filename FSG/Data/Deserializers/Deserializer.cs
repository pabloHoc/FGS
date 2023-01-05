using System.Text.Json;
using System.Text.Json.Serialization;

namespace FSG.Data
{
    public class Deserializer
    {
        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                IncludeFields = true,
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new ValueObjectConverterFactory(),
                    new EntityIdConverterFactory(),
                    new JsonStringEnumConverter(),
                    new ObjectToInferredTypesConverter()
                }   
            });
        }
    }
}