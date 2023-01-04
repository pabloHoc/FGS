using System;
using System.Collections.Generic;
using FSG.Data;
using FSG.Definitions;

namespace FSG.Core
{
    public class DefinitionRepository
    {
        private interface IDefinitionDictionary { }

        private class DefinitionDictionary<TDefinition> : Dictionary<string, TDefinition>, IDefinitionDictionary
          where TDefinition : IDefinition
        { }

        private readonly Dictionary<Type, IDefinitionDictionary> _definitions =
          new Dictionary<Type, IDefinitionDictionary>(){
            { typeof(LandDefinition), new DefinitionDictionary<LandDefinition>() },
            { typeof(BuildingDefinition), new DefinitionDictionary<BuildingDefinition>() },
            { typeof(ResourceDefinition), new DefinitionDictionary<ResourceDefinition>() },
            { typeof(EconomicCategoryDefinition), new DefinitionDictionary<EconomicCategoryDefinition>() }
          };

        public void Add<T>(T definition) where T : IDefinition
        {
            ((DefinitionDictionary<T>)this._definitions[typeof(T)]).Add(definition.Name, (T)definition);
        }

        public T Get<T>(string name) where T : IDefinition
        {
            return ((DefinitionDictionary<T>)this._definitions[typeof(T)])[name];
        }

        public List<T> GetAll<T>() where T : IDefinition
        {
            return new List<T>(((DefinitionDictionary<T>)this._definitions[typeof(T)]).Values);
        }

        public void Remove<T>(string name) where T : IDefinition
        {
            ((DefinitionDictionary<T>)this._definitions[typeof(T)]).Remove(name);
        }
    }
}