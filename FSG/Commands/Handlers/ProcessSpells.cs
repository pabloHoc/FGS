using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class ProcessSpells : CommandHandler<Commands.ProcessSpells>
    {
        public ProcessSpells(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ProcessSpells command)
        {
            var spells = _serviceProvider.GlobalState.World.Spells;

            foreach (var spell in spells)
            {
                spell.RemainingTurns--;

                if (spell.RemainingTurns == 0)
                {
                    if (spell.TargetType == EntityType.Empire)
                    {
                        // TODO: SpellService.Destroy
                        _serviceProvider.GlobalState.World.Empires
                            .Find(empire => empire.Id == (EntityId<Empire>)spell.TargetId)
                            .Modifiers.RemoveAll(modifier => (EntityId<Spell>)modifier.SourceId == spell.Id);
                    }

                    if (spell.TargetType == EntityType.Region)
                    {
                        _serviceProvider.GlobalState.World.Regions
                            .Find(region => region.Id == (EntityId<Region>)spell.TargetId)
                            .Modifiers.RemoveAll(modifier => (EntityId<Spell>)modifier.SourceId == spell.Id);
                    }

                    _serviceProvider.GlobalState.World.Spells.Remove(spell);
                }
            }
        }
    }
}

