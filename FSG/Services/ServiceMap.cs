using System;
using FSG.Core;

namespace FSG.Services
{
	public class ServiceMap
	{
		public BuildingService BuildingService { get; }

		public SpellService SpellService { get; }

		public ServiceMap(ServiceProvider serviceProvider)
		{
			BuildingService = new BuildingService(serviceProvider);
            SpellService = new SpellService(serviceProvider);
		}
	}
}

