using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using FSG;
using FSG.Common;
using FSG.Serialization;
using FSG.Entities;
using static FSG.Core.EntityRepository;

//using var game = new GameApp();
//game.Run();

// REPOSITORY SERIALIZATION ----------------------------------- 

//var entities = game._game.ServiceProvider.GlobalState.Entities._entities;

//var serializerOptions = new JsonSerializerOptions
//{
//    Converters =
//    {
//        new ValueObjectConverterFactory(),
//        new EntityIdConverterFactory(),
//        new JsonStringEnumConverter()
//    }
//};

//// act
//string json = JsonSerializer.Serialize(entities, serializerOptions);

//File.WriteAllText("../../../entities.json", json);

//var deserialized = JsonSerializer.Deserialize<EntityDictionaryMap>(json, serializerOptions);

// NESTED DICTIONARY SERIALIZATION -----------------------------------

//var dictionary = new Dictionary<string, object>
//{
//    { "sarasa", new Dictionary<string, object>
//        {
//            { "falopa", new Dictionary<string, object>
//                {
//                    { "test", "etc" }
//                }
//            }
//        }
//    }
//};

//string json = JsonSerializer.Serialize(dictionary);

//File.WriteAllText("../../../entities.json", json);

System.Console.WriteLine("Running");
