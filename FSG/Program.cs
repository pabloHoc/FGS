using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using FSG;
using FSG.Common;
using FSG.Serialization;
using FSG.Entities;
using FSG.Core;

var game = new Game();
game.Initialize();

// REPOSITORY SERIALIZATION ----------------------------------- 

var world = game.ServiceProvider.GlobalState.World;

var serializerOptions = new JsonSerializerOptions
{
    Converters =
    {
        new ValueObjectConverterFactory(),
        new EntityIdConverterFactory(),
        new JsonStringEnumConverter()
    }
};

// act
string json = JsonSerializer.Serialize(world, serializerOptions);

File.WriteAllText("../../../entities.json", json);

//var deserialized = JsonSerializer.Deserialize(json, serializerOptions);

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
