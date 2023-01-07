using System;
using System.Collections.Generic;

namespace FSG.Conditions
{
	public class ConditionMap
	{
		private readonly Dictionary<string, Func<object, ICondition>> _conditions =
			new Dictionary<string, Func<object, ICondition>>
			{
				// Land Conditions
				{ "is_land_type", (object value) => new IsLandType((string)value) },
				// Region Conditions
				{ "has_empire", (object value) => new HasEmpire((bool)value) }
			};

		public ICondition Get(string condition, object value)
		{
			return (ICondition)_conditions[condition](value);
		}

		public bool Has(string condition)
		{
			return _conditions.ContainsKey(condition);
		}
    }
}

