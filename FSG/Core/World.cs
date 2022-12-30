namespace FSG.Core
{
    public class World
    {
        private ServiceProvider _serviceProvider { get; }

        public World(ServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }
    }
}