using System.Collections.Generic;
using System.IO;
using FSG.Core;
using FSG.Definitions;

namespace FSG.Data
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
            this.LoadDefinition<ResourceDefinition>("../../../Assets/Definitions/Resources/resources.json");
            this.LoadDefinition<BuildingDefinition>("../../../Assets/Definitions/Buildings/buildings.json");
            this.LoadDefinition<LandDefinition>("../../../Assets/Definitions/Lands/lands.json");
            this.LoadDefinition<EconomicCategoryDefinition>("../../../Assets/Definitions/EconomicCategories/economic-categories.json");
            this.LoadDefinition<SpellDefinition>("../../../Assets/Definitions/Spells/spells.json");
            this.LoadDefinition<ScorerDefinition>("../../../Assets/Definitions/Scorers/scorers.json");
            this.LoadDefinition<TaskDefinition>("../../../Assets/Definitions/Tasks/test.json");
        }
    }
}