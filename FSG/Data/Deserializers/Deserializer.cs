using System.Text.Json;

namespace FSG.Data
{
    public class Deserializer
    {
        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                IncludeFields = true,
                PropertyNameCaseInsensitive = true
            });
        }
    }
}