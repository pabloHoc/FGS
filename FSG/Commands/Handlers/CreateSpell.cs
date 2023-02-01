using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreateSpell : CommandHandler<Commands.CreateSpell>
    {
        public CreateSpell(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.CreateSpell command)
        {
            var spell = new Spell
            {
                Id = new EntityId<Spell>(),
                Name = command.SpellName,
                TargetId = command.TargetId,
                TargetType = command.TargetType,
                RemainingTurns = command.Duration
            };
            this._serviceProvider.GlobalState.World.Spells.Add(spell);
            this._serviceProvider.GlobalState.World.LastAddedEntityId = spell.Id;
        }
    }
}