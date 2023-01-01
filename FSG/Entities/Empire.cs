// TODO: Add resources and production

namespace FSG.Entities
{
    public class Empire : IEntity<Empire>, INameableEntity
    {
        public EntityType Type { get; } = EntityType.Empire;

        public EntityId<Empire> Id { get; init; }

        public string Name { get; init; }
    }
}