using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreateDistrict : CommandHandler<Commands.CreateDistrict>
    {
        public CreateDistrict(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.CreateDistrict command)
        {
            var region = _serviceProvider.GlobalState.Entities.Get<Region>(command.RegionId);

            region.Capital.Districts.Add(new District
            {
                Name = command.DistrictName
            });
        }
    }
}

