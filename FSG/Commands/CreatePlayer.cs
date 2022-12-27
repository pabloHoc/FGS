using FSG.Entities;

namespace FSG.Commands
{
    public struct CreatePlayer : ICommand
    {
        public string Action { get => "CREATE_PLAYER"; }

        public string Name { get; init; }

        public EntityId<Empire> EmpireId { get; init; }

        public bool IsAI { get; init; }
    }
}