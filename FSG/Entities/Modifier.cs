namespace FSG.Entities
{
    public enum ModifierType
    {
        Add,
        Mult,
        Reduction
    }

    public class Modifier : IEntity<Modifier>, ITemporary
    {
        public EntityType Type => EntityType.Modifier;

        public EntityId<Modifier> Id { get; init; }

        public string Name { get; init; }

        public ModifierType ModifierType { get; init; }

        public decimal Value { get; init; }

        public int RemainingTurns { get; set; }

        public IEntityId TargetId { get; init; }

        public IEntityId SourceId { get; init; }

    }
}