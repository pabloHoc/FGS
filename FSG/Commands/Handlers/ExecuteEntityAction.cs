using System;
using FSG.AI;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class ExecuteEntityAction<T> : CommandHandler<Commands.ExecuteEntityAction<T>> where T : IEntity<T>, IActor, ILocatable
    {
        public ExecuteEntityAction(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ExecuteEntityAction<T> command)
        {
            var entity = _serviceProvider.GlobalState.Entities.Get(command.EntityId);

            if (command.ActionType == ActionType.Spell)
            {
                var spell = _serviceProvider.Definitions.Get<SpellDefinition>(command.ActionName);
                _serviceProvider.Services.SpellService.Execute(entity, spell);
            } else if (command.ActionName == "Move")
            {
                var regions = _serviceProvider.GlobalState.Entities.GetAll<Region>();
                var nodes = new RegionToNodeConverter(regions).GetNodes();
                var start = nodes.Find(node => node.Id == entity.RegionId);
                var end = nodes.Find(node => node.Id == command.Payload);

                var path = new Pathfinder(nodes).FindPath(start, end);

                _serviceProvider.Dispatcher.Dispatch(new Commands.SetLocation<T>
                {
                    EntityId = entity.Id,
                    EntityType = EntityType.Agent, // TODO: review this
                    RegionId = new EntityId<Region>(path[1].Id)
                });

                if (path.Count > 2)
                {
                    _serviceProvider.Dispatcher.Dispatch(new Commands.SetEntityCurrentAction<T>
                    {
                        EntityId = entity.Id,
                        EntityType = EntityType.Agent, // TODO: review this
                        NewCurrentAction = new ActionQueueItem
                        {
                            Name = "Move",
                            ActionType = ActionType.Action,
                            RemainingTurns = 1,
                            Payload = new EntityId<Region>(end.Id)
                        }
                    });
                } 
            }
        } 
    }
}

