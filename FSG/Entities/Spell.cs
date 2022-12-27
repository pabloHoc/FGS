namespace FSG.Entities
{
    public class Spell : IEntityWithTurns<Spell>
    {
        public string Type { get => "SPELL"; }

        public EntityId<Spell> Id { get; init; }

        public string Name { get; init; }

        public IEntityId TargetId { get; init; }

        public int RemainingTurns { get; set; }
    }
}