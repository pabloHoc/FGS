using System;
using FSG.Commands;
using FSG.Serialization;
using FSG.Entities;

namespace FSG.Core
{
    public class Game
    {
        public ServiceProvider ServiceProvider { get; } = new ServiceProvider();

        public void Initialize()
        {
            LoadDefinitions();
            LoadWorld();
            Start();
        }

        private void LoadDefinitions()
        {
            var loader = new DefinitionLoader(ServiceProvider.Definitions);
            loader.LoadDefinitions();
        }

        private void LoadWorld()
        {
            var options = new WorldGenerationOptions
            {
                Players = 20,
                LandsPerRegion = 5,
                Columns = 50,
                Rows = 50
            };

            ServiceProvider.Dispatcher.Dispatch(new GenerateWorld { Options = options });
        }

        private void Start()
        {
            ServiceProvider.Dispatcher.Dispatch(new StartGame());
        }
    }
}