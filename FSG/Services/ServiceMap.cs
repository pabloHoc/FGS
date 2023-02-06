using System;
using FSG.Core;

namespace FSG.Services
{
	public class ServiceMap
	{
        public ActionService ActionService { get; }

		public BuildingService BuildingService { get; }

		public EconomicCategoryService EconomicCategoryService { get; }

        public SpellService SpellService { get; }

		public TaskService TaskService { get; }

		public ServiceMap(ServiceProvider serviceProvider)
		{
            ActionService = new ActionService(serviceProvider);
            BuildingService = new BuildingService(serviceProvider);
			EconomicCategoryService = new EconomicCategoryService(serviceProvider);
			SpellService = new SpellService(serviceProvider);
			TaskService = new TaskService(serviceProvider);
		}
	}
}

