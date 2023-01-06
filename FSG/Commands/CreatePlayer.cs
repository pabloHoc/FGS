using FSG.Entities;

namespace FSG.Commands
{
    public struct CreatePlayer : ICommand
    {
        public string Name { get => "CreatePlayer"; }

        public string PlayerName { get; init; }

        public EntityId<Empire> EmpireId { get; init; }

        public bool IsAI { get; init; }
    }
}