using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class GenerateWorld : CommandHandler<Commands.GenerateWorld>
    {
        private readonly Dictionary<int, EntityId<Region>> _chunks = new Dictionary<int, EntityId<Region>>();

        public GenerateWorld(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.GenerateWorld command)
        {
            GeneratePlayersAndEmpires(command.Options.Players);
            GenerateRegions(command.Options.Columns, command.Options.Rows);
            GenerateRoads(command.Options.Columns, command.Options.Rows);
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
            var CHUNK_SIZE = 100;
            var BORDER_GAP = 20;

            var random = new Random(); // TODO: Get central instance of random generator so it can be seeded

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var createRegion = random.NextSingle() > 0.5f;

                    if (createRegion)
                    {
                        var x = random.Next(j * CHUNK_SIZE + BORDER_GAP, (j + 1) * CHUNK_SIZE - BORDER_GAP);
                        var y = random.Next(i * CHUNK_SIZE + BORDER_GAP, (i + 1) * CHUNK_SIZE - BORDER_GAP);

                        _serviceProvider.Dispatcher.Dispatch(new Commands.CreateRegion
                        {
                            RegionName = $"Region #{count}",
                            EmpireId = empires.Length > count ? empires[count].Id : null,
                            X = x,
                            Y = y
                        });

                        var regionId = _serviceProvider.GlobalState.Entities.GetLastAddedEntityId<Region>();

                        _chunks.Add(i * rows + j, regionId);

                        count++;
                    }
                }
            }
        }

        private void GenerateRoads(int columns, int rows)
        {
            var chunkIndexes = new List<int>(_chunks.Keys);

            // Horizontal roads
            for (int i = 0; i < chunkIndexes.Count; i++)
            {
                if (i < chunkIndexes.Count - 1)
                {
                    var chunkIndex = chunkIndexes[i];
                    var nextChunkIndex = chunkIndexes[i + 1];

                    var sameRow = Math.Floor((double) chunkIndex / columns) == Math.Floor((double) nextChunkIndex / columns);
                    var close = nextChunkIndex - chunkIndex < 3;

                    if (sameRow && close)
                    {
                        var region = _serviceProvider.GlobalState.Entities.Get(_chunks[chunkIndex]);
                        var nextRegion = _serviceProvider.GlobalState.Entities.Get(_chunks[nextChunkIndex]);

                        region.ConnectedTo.Add(nextRegion.Id);
                        nextRegion.ConnectedTo.Add(region.Id);
                    }
                }
            }
        }

        private void GenerateLands(int landsPerRegion)
        {
            var regions = _serviceProvider.GlobalState.Entities.GetAll<Region>();
            var landDefinitions = _serviceProvider.Definitions.GetAll<LandDefinition>().ToArray();
            var random = new Random();

            foreach (var region in regions)
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

