using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Conditions
{
	public class HasEmpire : ICondition<Region>
	{
		private readonly bool _value;

		public HasEmpire(bool value)
		{
			_value = value;
		}

		public bool IsValid(GameState gameState, Region region)
        {
			return (region.EmpireId != null) == _value;
		}
    }
}

