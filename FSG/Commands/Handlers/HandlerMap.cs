using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Entities;

// TODO: Check if the Dictionary should be a switch case (or something similar)
// maybe handlers needs to be static
// TODO: review use of generic here, maybe is better to handle it as a switch
// depending on EntityType inside the handler

namespace FSG.Commands.Handlers
{
    public class HandlerMap
    {
        private readonly Dictionary<Type, IBaseCommandHandler> _handlers;

        public HandlerMap(ServiceProvider serviceProvider)
        {
            this._handlers = new Dictionary<Type, IBaseCommandHandler>()
            {
                { typeof(Commands.BuildBuilding),  new BuildBuilding(serviceProvider) },
                { typeof(Commands.BuildBuildingFromQueue),  new BuildBuildingFromQueue(serviceProvider) },
                { typeof(Commands.ComputeEmpiresProduction), new ComputeEmpiresProduction(serviceProvider) },
                { typeof(Commands.ComputeRegionsProduction), new ComputeRegionsProduction(serviceProvider) },
                { typeof(Commands.CreateAgent),  new CreateAgent(serviceProvider) },
                { typeof(Commands.CreateArmy),  new CreateArmy(serviceProvider) },
                { typeof(Commands.CreateEmpire), new CreateEmpire(serviceProvider) },
                { typeof(Commands.CreateLand),  new CreateLand(serviceProvider) },
                { typeof(Commands.CreateModifier),  new CreateModifier(serviceProvider) },
                { typeof(Commands.CreatePlayer), new CreatePlayer(serviceProvider) },
                { typeof(Commands.CreatePop), new CreatePop(serviceProvider) },
                { typeof(Commands.CreateRegion), new CreateRegion(serviceProvider) },
                { typeof(Commands.CreateSpell), new CreateSpell(serviceProvider) },
                { typeof(Commands.EndTurn), new EndTurn(serviceProvider) },
                { typeof(Commands.ExecuteCurrentEntityAction), new ExecuteCurrentEntityAction(serviceProvider) },
                { typeof(Commands.ExecuteEntityAction), new ExecuteEntityAction(serviceProvider) },
                { typeof(Commands.GenerateWorld), new GenerateWorld(serviceProvider) },
                { typeof(Commands.MoveEntity), new MoveEntity(serviceProvider) },
                { typeof(Commands.ProcessBuildingQueues), new ProcessBuildingQueues(serviceProvider) },
                { typeof(Commands.ProcessEntityActions), new ProcessEntityActions(serviceProvider) },
                { typeof(Commands.ProcessPlayerAIs), new ProcessPlayerAIs(serviceProvider) },
                { typeof(Commands.ProcessPopsGrowth), new ProcessPopsGrowth(serviceProvider) },
                { typeof(Commands.ProcessResourceLevels), new ProcessResourceLevels(serviceProvider) },
                { typeof(Commands.ProcessSpells), new ProcessSpells(serviceProvider) },
                { typeof(Commands.QueueBuilding),  new QueueBuilding(serviceProvider) },
                { typeof(Commands.QueueEntityAction),  new QueueEntityAction(serviceProvider) },
                { typeof(Commands.SetEntityCurrentAction), new SetEntityCurrentAction(serviceProvider) },
                { typeof(Commands.SetLocation), new SetLocation(serviceProvider) },
                { typeof(Commands.SetOwnerEmpire), new SetOwnerEmpire(serviceProvider) },
                { typeof(Commands.StartGame), new StartGame(serviceProvider) },
                { typeof(Commands.UpdateEmpiresResources), new UpdateEmpiresResources(serviceProvider) },
                { typeof(Commands.UpdateRegionsResources), new UpdateRegionsResources(serviceProvider) },
            };
        }

        public void Handle<T>(T command) where T : ICommand
        {
            ((dynamic)this._handlers[command.GetType()]).Handle((dynamic)command);
        }
    }
}