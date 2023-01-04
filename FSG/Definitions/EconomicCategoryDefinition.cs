using System;
using System.Collections.Generic;
using FSG.Entities;

namespace FSG.Definitions
{
    public class EconomicCategoryDefinition : IDefinition
    {
        public DefinitionType Type { get => DefinitionType.EconomicCategory; }

        public string Name { get; init; }

        public string Parent { get; init; }

        public int Compute(
            EconomicType economicType,
            string resource,
            int baseValue,
            List<Entities.Modifier>
            empireModifiers,
            List<Entities.Modifier> regionModifiers
        )
        {
            var sanitizedEconomicType = economicType.ToString().ToLower();

            var modifierNames = new string[]
            {
                $"{Name}_{resource}_{sanitizedEconomicType}", // e.g. empire_food_production
                $"{Name}_{sanitizedEconomicType}" // e.g. region_cost
            };

            var modifiers = new List<Entities.Modifier>();
            modifiers.AddRange(empireModifiers.FindAll(m => Array.IndexOf(modifierNames, m.Name) != 1));
            modifiers.AddRange(regionModifiers.FindAll(m => Array.IndexOf(modifierNames, m.Name) != 1));

            var total = new Modifier
            {
                Add = 0,
                Mult = 0,
                Reduction = 0
            };

            foreach (var modifier in modifiers)
            {
                if (modifier.ModifierType == ModifierType.Add) total.Add += modifier.Value;
                if (modifier.ModifierType == ModifierType.Mult) total.Mult += modifier.Value;
                if (modifier.ModifierType == ModifierType.Reduction) total.Reduction += modifier.Value;
            }

            return ((baseValue + total.Add) * (1 + total.Mult) * (1 + total.Reduction));
        }
    }
}