using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class UpdateEmpiresResources : CommandHandler<Commands.UpdateEmpiresResources>
    {
        public UpdateEmpiresResources(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.UpdateEmpiresResources command)
        {
            var empires = _serviceProvider.GlobalState.Entities.GetAll<Empire>();

            foreach (var empire in empires)
            {
                foreach (var entry in empire.Resources.Resources)
                {
                    empire.Resources.Resources[entry.Key] += empire.Resources.Production[entry.Key];
                    empire.Resources.Resources[entry.Key] -= empire.Resources.Upkeep[entry.Key];
                }
            }
        }
    }
}

