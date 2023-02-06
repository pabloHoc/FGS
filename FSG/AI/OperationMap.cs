using System;
using System.Collections.Generic;
using FSG.AI.Operations;
using FSG.Core;
using FSG.Entities;
using FSG.UtilityAI;

namespace FSG.AI
{
	public class OperationMap
    {
		private readonly Dictionary<string, Operation<GameState, IBaseEntity>> _operationMap;

        public OperationMap(ServiceProvider serviceProvider)
		{
            _operationMap = new Dictionary<string, Operation<GameState, IBaseEntity>>
			{
				{ "SendAgentToConquestRegion", new SendAgentToConquestRegion(serviceProvider) }
			};
		}

		public Operation<GameState, IBaseEntity> Get(string operationName)
		{
			return _operationMap[operationName];
		}
	}
}

