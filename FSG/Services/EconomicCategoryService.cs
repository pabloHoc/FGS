﻿using System;
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
            public decimal Add;

            public decimal Mult;

            public decimal Reduction;
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

        public decimal Compute(
            EconomicCategoryDefinition economicCategory,
            EconomicType economicType,
            string resource,
            decimal baseValue,
            List<Entities.Modifier> empireModifiers,
            List<Entities.Modifier> regionModifiers
        )
        {
            var modifierNames = new string[]
            {
                $"{economicCategory.Name}{resource}{economicType.ToString()}", // e.g. EmpireFoodProduction
                $"{economicCategory.Name}{economicType.ToString()}" // e.g. EmpireProduction
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

