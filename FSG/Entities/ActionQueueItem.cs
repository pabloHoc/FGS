// TODO: Add payload

namespace FSG.Entities
{
    public enum ActionType
    {
        Action,
        Spell
    }

    public class ActionQueueItem : IEntity<ActionQueueItem>, IEntityWithTurns
    {
        public EntityType Type { get; } = EntityType.ActionQueueItem;

        public EntityId<ActionQueueItem> Id { get; init; }

        public string Name { get; init; }

        public ActionType ActionType { get; init; }

        public int RemainingTurns { get; set; }
    }
}