namespace FSG.Entities
{
    public enum ModifierType
    {
        Add,
        Mult,
        Reduction
    }

    public class Modifier : IEntity<Modifier>, IEntityWithTurns
    {
        public EntityType Type { get; } = EntityType.Modifier;

        public EntityId<Modifier> Id { get; init; }

        public string Name { get; init; }

        public ModifierType ModifierType { get; init; }

        public int Value { get; init; }

        public IEntityId EntityId { get; init; }

        public int RemainingTurns { get; set; }

        public IEntityId SourceId { get; init; }
    }
}