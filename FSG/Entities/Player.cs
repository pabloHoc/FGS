// TODO: add ai

namespace FSG.Entities
{
    public class Player : IEntity<Player>
    {
        public EntityType Type => EntityType.Player;

        public EntityId<Player> Id { get; init; }

        public string Name { get; init; }

        public EntityId<Empire> EmpireId { get; init; }

        public bool IsAI { get; init; }
    }
}