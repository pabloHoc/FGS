using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct CreatePop: ICommand
	{
		public string Name { get => "CreatePop"; }

		public EntityId<Region> RegionId { get; init; }

		public string Strata { get; init; }

        public CreatePop(dynamic payload)
        {
            Strata = payload["Strata"];
            RegionId = payload["RegionId"];
        }
    }
}

