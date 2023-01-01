namespace FSG.Entities
{
    public class Spell : IEntity<Spell>, ITemporaryEntity
    {
        public EntityType Type { get; } = EntityType.Spell;

        public EntityId<Spell> Id { get; init; }

        public string Name { get; init; }

        public IEntityId TargetId { get; init; }

        public int RemainingTurns { get; set; }
    }
}