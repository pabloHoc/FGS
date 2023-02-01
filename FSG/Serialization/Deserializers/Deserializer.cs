using System.Text.Json;
using System.Text.Json.Serialization;

namespace FSG.Serialization
{
    public class Deserializer
    {
        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                IncludeFields = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                ReferenceHandler = ReferenceHandler.Preserve,
                Converters =
                {
                    new ValueObjectConverterFactory(),
                    new EntityIdConverterFactory(),
                    new JsonStringEnumConverter(),
                    new ObjectToInferredTypesConverter
                    (
                        floatFormat : FloatFormat.Double,
                        unknownNumberFormat : UnknownNumberFormat.Error,
                        objectFormat : ObjectFormat.Dictionary
                    )
                }   
            });
        }
    }
}