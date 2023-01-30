namespace FSG.Entities
{
    public class Spell : IEntity<Spell>, ITemporary
    {
        public EntityType EntityType => EntityType.Spell;

        public EntityId<Spell> Id { get; set; }

        public string Name { get; init; }

        public IEntityId TargetId { get; init; }

        public int RemainingTurns { get; set; }
    }
}