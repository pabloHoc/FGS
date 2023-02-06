using System;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;
using FSG.UtilityAI;

namespace FSG.AI
{
	public class AI
	{
		private readonly Player _player;
		private readonly ServiceProvider _serviceProvider;

		public AI(Player player, ServiceProvider serviceProvider)
		{
			_player = player;
			_serviceProvider = serviceProvider;
		}

		public void PlayTurn()
		{
            IDomain<GameState, IBaseEntity> domain = new Domain(_serviceProvider, _player);

			var planner = new Planner<GameState, IBaseEntity>(domain);

			planner.GeneratePlan();
			planner.ExecutePlan();
		}
	}
}

