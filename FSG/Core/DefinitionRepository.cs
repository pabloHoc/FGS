using System;
using System.Collections.Generic;
using FSG.Data;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Core
{
    public class DefinitionRepository
    {
        private class DefinitionDictionaryMap
        {
            public Dictionary<string, LandDefinition> LandDefinition { get; } = new Dictionary<string, LandDefinition>();
            public Dictionary<string, BuildingDefinition> BuildingDefinition { get; } = new Dictionary<string, BuildingDefinition>();
            public Dictionary<string, ResourceDefinition> ResourceDefinition { get; } = new Dictionary<string, ResourceDefinition>();
            public Dictionary<string, EconomicCategoryDefinition> EconomicCategoryDefinition { get; } = new Dictionary<string, EconomicCategoryDefinition>();
            public Dictionary<string, SpellDefinition> SpellDefinition { get; } = new Dictionary<string, SpellDefinition>();

            public Dictionary<string, T> Get<T>() where T : IDefinition
            {
                return (Dictionary<string, T>)this.GetType().GetProperty(typeof(T).Name).GetValue(this);
            }
        }

        private readonly DefinitionDictionaryMap _definitions = new DefinitionDictionaryMap();

        public void Add<T>(T definition) where T : IDefinition
        {
            this._definitions.Get<T>().Add(definition.Name, (T)definition);
        }

        public T Get<T>(string name) where T : IDefinition
        {
            return this._definitions.Get<T>()[name];
        }

        public List<T> GetAll<T>() where T : IDefinition
        {
            return new List<T>(this._definitions.Get<T>().Values);
        }

        public void Remove<T>(string name) where T : IDefinition
        {
            this._definitions.Get<T>().Remove(name);
        }
    }
}