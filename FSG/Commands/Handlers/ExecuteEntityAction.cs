using System;
using FSG.AI;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class ExecuteEntityAction : CommandHandler<Commands.ExecuteEntityAction>
    {
        public ExecuteEntityAction(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ExecuteEntityAction command)
        {
            var entity = _serviceProvider.GlobalState.World.Agents.Find(agent => agent.Id == (EntityId<Agent>)command.EntityId);

            if (command.ActionName == "Move")
            {
                _serviceProvider.Dispatcher.Dispatch(new Commands.SetLocation
                {
                    EntityId = entity.Id,
                    EntityType = entity.EntityType,
                    RegionId = new EntityId<Region>(command.Payload)
                });
            }
            else if (command.ActionType == ActionType.Spell)
            {
                var spell = _serviceProvider.Definitions.Get<SpellDefinition>(command.ActionName);
                _serviceProvider.Services.SpellService.Execute(entity, spell);
            }
            else if (command.ActionType == ActionType.Action)
            {
                var action = _serviceProvider.Definitions.Get<AgentActionDefinition>(command.ActionName);
                _serviceProvider.Services.ActionService.Execute(entity, action);
            }
        } 
    }
}

