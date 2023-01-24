using System;
using FSG.Entities;
using System.Resources;
using FSG.Definitions;
using FSG.Core;

namespace FSG.Services
{
	public class BuildingService
	{
        private readonly ServiceProvider _serviceProvider;

        public BuildingService(ServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;
		}

        public bool Allow(Empire empire, Land land, BuildingDefinition definition)
        {
            // TODO: if (empire.CanAfford(building))
            foreach (var resource in definition.Resources.Cost)
            {
                if (empire.Resources.Resources[resource.Key] < resource.Value)
                {
                    return false;
                }
            }

            if (definition.Conditions != null)
            {
                if (!_serviceProvider.ConditionValidator.isValid(definition.Conditions, land))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

