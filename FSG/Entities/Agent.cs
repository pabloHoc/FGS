using System;
using System.Collections.Generic;

namespace FSG.Entities
{
    public class Agent : IEntity<Agent>, ILocatable, IOwneable, INameable, IActor
    {
        public EntityType EntityType => EntityType.Agent;

        public EntityId<Agent> Id { get; init; }

        public string Name { get; init; }

        public Empire Empire { get; set; }

        public Region Region { get; set; }

        public Queue<ActionQueueItem> Actions { get; init; }
    }
}