using System.Collections.Generic;
using System.IO;
using FSG.Core;

namespace FSG.Data
{
    public class DefinitionLoader
    {
        private DefinitionRepository _repository;

        public DefinitionLoader(DefinitionRepository repository)
        {
            this._repository = repository;
        }

        private void LoadDefinition<T>(string path) where T : IDefinition
        {
            var json = File.ReadAllText(path);
            var definitions = Deserializer.Deserialize<List<T>>(json);
            definitions.ForEach(this._repository.Add);
        }

        public void LoadDefinitions()
        {
            this.LoadDefinition<ResourceDefinition>("../../../Common/Resources/resources.json");
            this.LoadDefinition<BuildingDefinition>("../../../Common/Buildings/buildings.json");
            this.LoadDefinition<LandDefinition>("../../../Common/Lands/lands.json");
            this.LoadDefinition<EconomicCategoryDefinition>("../../../Common/EconomicCategories/economic-categories.json");
        }
    }
}