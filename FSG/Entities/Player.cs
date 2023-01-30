// TODO: add ai

using System.Text.Json.Serialization;

namespace FSG.Entities
{
    public class Player : IEntity<Player>
    {
        public EntityType EntityType => EntityType.Player;

        public EntityId<Player> Id { get; init; }

        public string Name { get; init; }

        public EntityId<Empire> EmpireId { get; init; }

        public bool IsAI { get; init; }

        [JsonIgnore]
        public FSG.AI.AI AI { get; set; }
    }
}