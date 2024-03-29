namespace FSG.Entities
{
    public enum ModifierType
    {
        Add,
        Mult,
        Reduction
    }

    // TODO: this doesn't need an Id, we could remove it by value
    
    public class Modifier : IEntity<Modifier>, ITemporary
    {
        public EntityType EntityType => EntityType.Modifier;

        public EntityId<Modifier> Id { get; init; }

        public string Name { get; init; }

        public ModifierType ModifierType { get; init; }

        public decimal Value { get; init; }

        public int RemainingTurns { get; set; }

        public IEntityId TargetId { get; init; }

        public IEntityId SourceId { get; init; }

    }
}