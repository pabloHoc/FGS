// TODO: Add payload

namespace FSG.Entities
{
    public enum ActionType
    {
        Action,
        Spell
    }

    public class ActionQueueItem : ITemporary
    {
        public string Name { get; init; }

        public ActionType ActionType { get; init; }

        public int RemainingTurns { get; set; }
    }
}