// TODO: Add resources and production

namespace FSG.Entities
{
    public class Empire : IEntity<Empire>
    {
        public string Type { get => "EMPIRE"; }

        public EntityId<Empire> Id { get; init; }

        public string Name { get; init; }
    }
}