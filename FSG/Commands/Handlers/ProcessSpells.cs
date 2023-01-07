using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class ProcessSpells : CommandHandler<Commands.ProcessSpells>
    {
        public ProcessSpells(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ProcessSpells command)
        {
            var spells = _serviceProvider.GlobalState.Entities.GetAll<Spell>();

            foreach (var spell in spells)
            {
                spell.RemainingTurns--;

                if (spell.RemainingTurns == 0)
                {
                    // TODO: SpellService.Destroy()
                    var spellModifiers = _serviceProvider.GlobalState.Entities
                        .GetAll<Modifier>().FindAll(m => ((dynamic)m.SourceId).Value == spell.Id);

                    foreach (var modifier in spellModifiers)
                    {
                        _serviceProvider.GlobalState.Entities.Remove(modifier);
                    }

                    _serviceProvider.GlobalState.Entities.Remove(spell);
                }
            }
        }
    }
}

