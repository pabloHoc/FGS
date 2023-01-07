using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreateSpell : CommandHandler<Commands.CreateSpell>
    {
        public CreateSpell(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.CreateSpell command)
        {
            this._serviceProvider.GlobalState.Entities.Add(new Spell
            {
                Id = new EntityId<Spell>(),
                Name = command.SpellName,
                TargetId = command.TargetId,
                RemainingTurns = command.Duration
            });
        }
    }
}