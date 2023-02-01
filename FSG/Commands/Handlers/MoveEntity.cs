using System;
using FSG.AI;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class MoveEntity : CommandHandler<Commands.MoveEntity>
    {
        public MoveEntity(ServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void Handle(Commands.MoveEntity command)
        {
            var entity = _serviceProvider.GlobalState.World.Agents
                .Find(agent => agent.Id == (EntityId<Agent>)command.EntityId);

            var regions = _serviceProvider.GlobalState.World.Regions;
            var nodes = new RegionToNodeConverter(regions).GetNodes();
            var start = nodes.Find(node => node.Id == entity.Region.Id);
            var end = nodes.Find(node => node.Id == command.RegionId);

            var path = new Pathfinder(nodes).FindPath(start, end);

            entity.Actions.Clear();

            foreach (var node in path)
            {
                entity.Actions.Enqueue(new ActionQueueItem
                {
                    ActionType = ActionType.Action,
                    Name = "Move",
                    RemainingTurns = 1,
                    Payload = node.Id
                });
            };
        }
    }
}

