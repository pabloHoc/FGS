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
                { typeof(Commands.AddBuildingtoQueue),  new AddBuildingToQueue(serviceProvider) },
                { typeof(Commands.BuildBuilding),  new BuildBuilding(serviceProvider) },
                { typeof(Commands.BuildBuildingFromQueue),  new BuildBuildingFromQueue(serviceProvider) },
                { typeof(Commands.CreateAgent),  new CreateAgent(serviceProvider) },
                { typeof(Commands.CreateArmy),  new CreateArmy(serviceProvider) },
                { typeof(Commands.CreateEmpire), new CreateEmpire(serviceProvider) },
                { typeof(Commands.CreateLand),  new CreateLand(serviceProvider) },
                { typeof(Commands.CreatePlayer), new CreatePlayer(serviceProvider) },
                { typeof(Commands.CreateRegion), new CreateRegion(serviceProvider) },
                { typeof(Commands.EndTurn), new EndTurn(serviceProvider) },
                { typeof(Commands.ExecuteCurrentEntityAction<Agent>), new ExecuteCurrentEntityAction<Agent>(serviceProvider) },
                { typeof(Commands.ExecuteEntityAction<Agent>), new ExecuteEntityAction<Agent>(serviceProvider) },
                { typeof(Commands.GenerateResources), new GenerateResources(serviceProvider) },
                { typeof(Commands.GenerateWorld), new GenerateWorld(serviceProvider) },
                { typeof(Commands.ProcessBuildingQueues), new ProcessBuildingQueues(serviceProvider) },
                { typeof(Commands.ProcessEntityActions<Agent>), new ProcessEntityActions<Agent>(serviceProvider) },
                { typeof(Commands.SetEntityCurrentAction<Agent>), new SetEntityCurrentAction<Agent>(serviceProvider) },
                { typeof(Commands.SetLocation<Agent>), new SetLocation<Agent>(serviceProvider) },
                { typeof(Commands.SetLocation<Army>), new SetLocation<Army>(serviceProvider) },
                { typeof(Commands.SetOwnerEmpire<Agent>), new SetOwnerEmpire<Agent>(serviceProvider) },
                { typeof(Commands.SetOwnerEmpire<Army>), new SetOwnerEmpire<Army>(serviceProvider) },
                { typeof(Commands.SetOwnerEmpire<Region>), new SetOwnerEmpire<Region>(serviceProvider) },
                { typeof(Commands.StartGame), new StartGame(serviceProvider) },
                { typeof(Commands.UpdateProduction), new UpdateProduction(serviceProvider) }
            };
        }

        public CommandHandler<T> GetFor<T>(T command) where T : ICommand
        {
            return (CommandHandler<T>)this._handlers[command.GetType()];
        }
    }
}