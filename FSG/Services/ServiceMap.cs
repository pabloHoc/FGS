using System;
using FSG.Core;

namespace FSG.Services
{
	public class ServiceMap
	{
		public BuildingService BuildingService { get; }

		public EconomicCategoryService EconomicCategoryService { get; }

		public ModifierService ModifierService { get; }

        public SpellService SpellService { get; }

		public ServiceMap(ServiceProvider serviceProvider)
		{
			BuildingService = new BuildingService(serviceProvider);
			EconomicCategoryService = new EconomicCategoryService(serviceProvider);
			ModifierService = new ModifierService(serviceProvider);
			SpellService = new SpellService(serviceProvider);
		}
	}
}

