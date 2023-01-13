using System.Collections.Generic;
using System.IO;
using FSG.Core;
using FSG.Definitions;

namespace FSG.Serialization
{
    public class DefinitionLoader
    {
        private readonly DefinitionRepository _repository;

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
            this.LoadDefinition<ResourceDefinition>("../../../../FSG/Assets/Definitions/Resources/resources.json");
            this.LoadDefinition<BuildingDefinition>("../../../../FSG/Assets/Definitions/Buildings/buildings.json");
            this.LoadDefinition<LandDefinition>("../../../../FSG/Assets/Definitions/Lands/lands.json");
            this.LoadDefinition<EconomicCategoryDefinition>("../../../../FSG/Assets/Definitions/EconomicCategories/economic-categories.json");
            this.LoadDefinition<SpellDefinition>("../../../../FSG/Assets/Definitions/Spells/spells.json");
            this.LoadDefinition<ScorerDefinition>("../../../../FSG/Assets/Definitions/Scorers/scorers.json");
            this.LoadDefinition<TaskDefinition>("../../../../FSG/Assets/Definitions/Tasks/test.json");
        }
    }
}