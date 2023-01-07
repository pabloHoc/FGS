using System;
using FSG.Definitions;
using FSG.Entities;
using System.Collections.Generic;
using FSG.Core;

namespace FSG.Services
{
    public class EconomicCategoryService
    {
        private struct ModifierTotals
        {
            public int Add;

            public int Mult;

            public int Reduction;
        }

        private readonly ServiceProvider _serviceProvider;

        public EconomicCategoryService(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private ModifierTotals GetModifierTotals(List<Modifier> modifiers)
        {
            var total = new ModifierTotals
            {
                Add = 0,
                Mult = 0,
                Reduction = 0
            };

            foreach (var modifier in modifiers)
            {
                if (modifier.ModifierType == ModifierType.Add)
                    total.Add += modifier.Value;
                if (modifier.ModifierType == ModifierType.Mult)
                    total.Mult += modifier.Value;
                if (modifier.ModifierType == ModifierType.Reduction)
                    total.Reduction += modifier.Value;
            }

            return total;
        }

        public int Compute(
            EconomicCategoryDefinition economicCategory,
            EconomicType economicType,
            string resource,
            int baseValue,
            List<Entities.Modifier> empireModifiers,
            List<Entities.Modifier> regionModifiers
        )
        {
            var sanitizedEconomicType = economicType.ToString().ToLower();

            var modifierNames = new string[]
            {
                $"{economicCategory.Name}_{resource}_{sanitizedEconomicType}", // e.g. empire_food_production
                $"{economicCategory.Name}_{sanitizedEconomicType}" // e.g. region_cost
            };

            Predicate<Modifier> modifierInModifierNames = m => Array.IndexOf(modifierNames, m.Name) != -1;

            var modifiers = new List<Entities.Modifier>();
            modifiers.AddRange(empireModifiers.FindAll(modifierInModifierNames));
            modifiers.AddRange(regionModifiers.FindAll(modifierInModifierNames));

            var total = GetModifierTotals(modifiers);

            return ((baseValue + total.Add) * (1 + total.Mult) * (1 + total.Reduction));
        }
    }
}

