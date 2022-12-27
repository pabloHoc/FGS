// TODO: add ai

namespace FSG.Entities
{
    public class Player : IEntity<Player>
    {
        public string Type { get => "PLAYER"; }

        public EntityId<Player> Id { get; init; }

        public string Name { get; init; }

        public EntityId<Empire> EmpireId { get; init; }

        public bool IsAI { get; init; }
    }
}