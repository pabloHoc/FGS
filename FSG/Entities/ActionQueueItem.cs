// TODO: Add payload

namespace FSG.Entities
{
    public enum ActionType
    {
        Action,
        Spell
    }

    public class ActionQueueItem : IEntityWithTurns<ActionQueueItem>
    {
        public string Type { get => "ACTION_QUEUE_ITEM"; }

        public EntityId<ActionQueueItem> Id { get; init; }

        public string Name { get; init; }

        public ActionType ActionType { get; init; }

        public int RemainingTurns { get; set; }
    }
}