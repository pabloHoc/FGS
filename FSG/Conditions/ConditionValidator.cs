using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Entities;
using FSG.Scopes;

namespace FSG.Conditions
{
    public class ConditionValidator
    {
        private readonly ConditionMap _conditionMap = new ConditionMap();

        private readonly ServiceProvider _serviceProvider;

        public ConditionValidator(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public bool isValid<T, U>(U conditions, T scope) where T: IBaseEntity where U : Dictionary<string, object>
        {
            Scope scopeKey;

            foreach (var entry in conditions)
            {
                if (_conditionMap.Has(entry.Key))
                {
                    var condition = _conditionMap.Get(entry.Key, entry.Value);
                    var result = condition.IsValid(_serviceProvider.GlobalState, scope);

                    // TODO: check this comparison
                    if (result == false)
                    {
                        return false;
                    }
                }
                else if (Enum.TryParse(entry.Key, out scopeKey))
                {
                    var newScope = _serviceProvider.Scopes.GetFrom(scopeKey, scope);
                    var result = isValid((Dictionary<string, object>)entry.Value, newScope);

                    if (result == false)
                    {
                        return false;
                    }
                }
                else
                {
                    throw new Exception($"INVALID CONDITION KEY {entry.Key}");
                }
            }

            return true;
        }
    }
}

