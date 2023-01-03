using System.Collections.Generic;
using FSG.Data;

namespace FSG.Entities
{
    public class Empire : IEntity<Empire>, INameableEntity
    {
        public EntityType Type { get; } = EntityType.Empire;

        public EntityId<Empire> Id { get; init; }

        public string Name { get; init; }

        public Dictionary<string, int> Resources { get; init; }

        public Dictionary<string, int> Production { get; init; }

        public List<Modifier> Modifiers { get; init; }
    }
}