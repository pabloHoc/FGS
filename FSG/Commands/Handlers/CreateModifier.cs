using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreateModifier : CommandHandler<Commands.CreateModifier>
    {
        public CreateModifier(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.CreateModifier command)
        {
            this._serviceProvider.GlobalState.Entities.Add(new Modifier
            {
                Id = new EntityId<Modifier>(),
                Name = command.ModifierName,
                ModifierType = command.ModifierType,
                Value = command.Value,
                TargetId = command.TargetId,
                SourceId = command.SourceId,
                RemainingTurns = command.Duration
            });
        }
    }
}