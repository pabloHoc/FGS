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
            GeneratePops();
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

                        var createRegionCommand = new Commands.CreateRegion
                        {
                            RegionName = $"Region #{count}",
                            X = x,
                            Y = y
                        };

                        if (empires.Length > count)
                        {
                            createRegionCommand.EmpireId = empires[count].Id;
                        };

                        _serviceProvider.Dispatcher.Dispatch(createRegionCommand);

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
                    var chunk = chunkIndexes[i];
                    var nextChunk = chunkIndexes[i + 1];

                    var sameRow = Math.Floor((double)chunk / columns) == Math.Floor((double)nextChunk / columns);
                    var close = nextChunk - chunk < 3;

                    if (sameRow && close)
                    {
                        var region = _serviceProvider.GlobalState.Entities.Get(_chunks[chunk]);
                        var nextRegion = _serviceProvider.GlobalState.Entities.Get(_chunks[nextChunk]);

                        region.ConnectedTo.Add(nextRegion.Id);
                        nextRegion.ConnectedTo.Add(region.Id);
                    }
                }
            }

            // Vertical roads
            for (int i = 0; i < chunkIndexes.Count; i++)
            {
                var chunk = chunkIndexes[i];
                var bottomChunk = chunk + columns;
                var bottomLeftChunk = bottomChunk - 1;
                var bottomRightChunk = bottomChunk + 1;
                var twoRowsBottomChunk = chunk + columns * 2;
                var twoRowsBottomLeftChunk = twoRowsBottomChunk - 1;
                var twoRowsBottomRightChunk = twoRowsBottomChunk + 1;

                var regionId = _chunks[chunk];
                var region = _serviceProvider.GlobalState.Entities.Get(_chunks[chunk]);

                var isLeftBorder = chunk % columns == 0;
                var isRightBorder = (chunk + 1) % columns == 0;
                var isBorder = isLeftBorder || isRightBorder;

                var chunksToConnect = new List<int>();

                // Try to connect bottom first
                if (_chunks.ContainsKey(bottomChunk))
                {
                    chunksToConnect.Add(bottomChunk);
                }
                // If not, try to connect bottom diagonals
                else if (!isBorder)
                {
                    if (_chunks.ContainsKey(bottomLeftChunk))
                    {
                        chunksToConnect.Add(bottomLeftChunk);
                    }

                    if (_chunks.ContainsKey(bottomRightChunk))
                    {
                        chunksToConnect.Add(bottomRightChunk);
                    }
                }
                // If left border, connect right diagonal only
                else if (isLeftBorder && !isRightBorder && _chunks.ContainsKey(bottomRightChunk))
                {
                    chunksToConnect.Add(bottomRightChunk);
                }
                // If right border, connect left diagonal only
                else if (!isLeftBorder && isRightBorder && _chunks.ContainsKey(bottomLeftChunk))
                {
                    chunksToConnect.Add(bottomLeftChunk);
                }
                // Do the same, two rows below
                else if (_chunks.ContainsKey(twoRowsBottomChunk))
                {
                    chunksToConnect.Add(twoRowsBottomChunk);
                }
                else if (!isBorder)
                {
                    if (_chunks.ContainsKey(twoRowsBottomLeftChunk))
                    {
                        chunksToConnect.Add(twoRowsBottomLeftChunk);
                    }

                    if (_chunks.ContainsKey(twoRowsBottomRightChunk))
                    {
                        chunksToConnect.Add(twoRowsBottomRightChunk);
                    }
                }
                else if (isLeftBorder && !isRightBorder && _chunks.ContainsKey(twoRowsBottomRightChunk))
                {
                    chunksToConnect.Add(twoRowsBottomRightChunk);
                }
                else if (!isLeftBorder && isRightBorder && _chunks.ContainsKey(twoRowsBottomLeftChunk))
                {
                    chunksToConnect.Add(twoRowsBottomLeftChunk);
                }

                foreach (var chunkToConnect in chunksToConnect)
                {
                    var nextRegion = _serviceProvider.GlobalState.Entities.Get(_chunks[chunkToConnect]);
                    region.ConnectedTo.Add(nextRegion.Id);
                    nextRegion.ConnectedTo.Add(region.Id);
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
            var regions = _serviceProvider.GlobalState.Entities.GetAll<Region>()
                .FindAll(region => region.EmpireId != null);
            var count = 0;

            foreach (var region in regions)
            {
                _serviceProvider.Dispatcher.Dispatch(new Commands.CreateAgent
                {
                    AgentName = $"Agent #{count}",
                    RegionId = region.Id,
                    EmpireId = region.EmpireId
                });

                count++;
            }
        }

        private void GeneratePops()
        {
            var regions = _serviceProvider.GlobalState.Entities.GetAll<Region>()
                .FindAll(region => region.EmpireId != null);

            foreach (var region in regions)
            {
                for (int i = 0; i < 5; i++)
                {
                    _serviceProvider.Dispatcher.Dispatch(new Commands.CreatePop
                    {
                        RegionId = region.Id,
                        Strata = "Peasant"
                    });
                }
            }
        }
    }
}

