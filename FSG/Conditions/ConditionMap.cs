using System;
using System.Collections.Generic;

namespace FSG.Conditions
{
	public class ConditionMap
	{
		private readonly Dictionary<string, Func<object, ICondition>> _conditions =
			new Dictionary<string, Func<object, ICondition>>
			{
				{ "IsLandType", (object value) => new IsLandType((string)value) }
			};

		public ICondition<T> Get<T>(string condition, object value)
		{
			return (ICondition<T>)_conditions[condition](value);
		}

		public bool Has(string condition)
		{
			return _conditions.ContainsKey(condition);
		}
    }
}

