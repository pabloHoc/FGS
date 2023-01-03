using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class GenerateResources : CommandHandler<Commands.GenerateResources>
    {
        public GenerateResources(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.GenerateResources command)
        {
            var empires = _serviceProvider.GlobalState.Entities.GetAll<Empire>();

            foreach (var empire in empires)
            {
                foreach (var entry in empire.Resources)
                {
                    empire.Resources[entry.Key] += empire.Production[entry.Key];
                }
            }
        }
    }
}

