using System;
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

        public bool isValid<T>(Conditions conditions, T scope) where T: IBaseEntity
        {
            Scope scopeKey;

            foreach (var entry in conditions)
            {
                if (_conditionMap.Has(entry.Key))
                {
                    var condition = _conditionMap.Get<T>(entry.Key, entry.Value);
                    var result = condition.IsValid(_serviceProvider.GlobalState, scope);

                    // TODO: check this comparison
                    if (result == false)
                    {
                        return false;
                    }
                } else if (Enum.TryParse(entry.Key, out scopeKey))
                {
                    var newScope = _serviceProvider.Scopes.GetFrom(scopeKey, scope);
                    var result = isValid((Conditions)entry.Value, newScope);

                    if (result == false)
                    {
                        return false;
                    }
                } else
                {
                    throw new Exception($"INVALID CONDITION KEY {entry.Key}");
                }
            }

            return true;
        }
    }
}

