using System;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class GenerateWorld : CommandHandler<Commands.GenerateWorld>
    {
        public GenerateWorld(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.GenerateWorld command)
        {
            GeneratePlayersAndEmpires(command.Options.Players);
            GenerateRegions(command.Options.Columns, command.Options.Rows);
            //GenerateRoads();
            GenerateLands(command.Options.LandsPerRegion);
            GenerateAgents();
        }

        private void GeneratePlayersAndEmpires(int players)
        {
            for (int i = 0; i < players; i++)
            {
                var isPlayer = i == 0;

                _serviceProvider.Dispatcher.Dispatch(new Commands.CreateEmpire
                {
                    EmpireName = isPlayer ? "Player Empire" : $"AI #{i} Empire"
                });

                _serviceProvider.Dispatcher.Dispatch(new Commands.CreatePlayer
                {
                    PlayerName = isPlayer ? "Player" : $"AI #{i}",
                    EmpireId = _serviceProvider.GlobalState.Entities.GetLastAddedEntityId<Empire>(),
                    IsAI = !isPlayer
                });
            }
        }

        private void GenerateRegions(int columns, int rows)
        {
            var count = 0;
            var empires = _serviceProvider.GlobalState.Entities.GetAll<Empire>().ToArray();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    _serviceProvider.Dispatcher.Dispatch(new Commands.CreateRegion
                    {
                        RegionName = $"Region #{count}",
                        EmpireId = empires.Length > count ? empires[count].Id : null,
                        X = j,
                        Y = i
                    });
                    count++;
                }
            }
        }

        private void GenerateRoads()
        {
            throw new NotImplementedException();
        }

        private void GenerateLands(int landsPerRegion)
        {
            var regions = _serviceProvider.GlobalState.Entities.GetAll<Region>();
            var landDefinitions = _serviceProvider.Definitions.GetAll<LandDefinition>().ToArray();
            var random = new Random();

            foreach(var region in regions)
            {
                for (int i = 0; i < landsPerRegion; i++)
                {
                    var landIndex = random.Next(0, landDefinitions.Length);
                    var landDefinition = landDefinitions[landIndex];

                    _serviceProvider.Dispatcher.Dispatch(new Commands.CreateLand
                    {
                        LandName = landDefinition.Name,
                        RegionId = region.Id
                    });
                }
            }
        }

        private void GenerateAgents()
        {
            var regions = _serviceProvider.GlobalState.Entities.GetAll<Region>();
            var count = 0;
            foreach (var region in regions)
            {
                if (region.EmpireId != null)
                {
                    _serviceProvider.Dispatcher.Dispatch(new Commands.CreateAgent
                    {
                        AgentName = $"Agent #{count}",
                        RegionId = region.Id,
                        EmpireId = (EntityId<Empire>)region.EmpireId
                    });

                    count++;
                }
            }
        }
    }
}

