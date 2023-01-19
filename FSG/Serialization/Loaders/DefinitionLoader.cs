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
            this.LoadDefinition<BuildingDefinition>("../../../../FSG/Assets/Definitions/Buildings/LandBuildings.json");
            this.LoadDefinition<BuildingDefinition>("../../../../FSG/Assets/Definitions/Buildings/CapitalBuildings.json");
            this.LoadDefinition<BuildingDefinition>("../../../../FSG/Assets/Definitions/Buildings/Districts.json");
            this.LoadDefinition<EconomicCategoryDefinition>("../../../../FSG/Assets/Definitions/EconomicCategories/EconomicCategories.json");
            this.LoadDefinition<LandDefinition>("../../../../FSG/Assets/Definitions/Lands/Lands.json");
            this.LoadDefinition<ScorerDefinition>("../../../../FSG/Assets/Definitions/Scorers/Scorers.json");
            this.LoadDefinition<SpellDefinition>("../../../../FSG/Assets/Definitions/Spells/Spells.json");
            this.LoadDefinition<StrataDefinition>("../../../../FSG/Assets/Definitions/Stratas/Stratas.json");
            this.LoadDefinition<ResourceDefinition>("../../../../FSG/Assets/Definitions/Resources/Resources.json");
            this.LoadDefinition<TaskDefinition>("../../../../FSG/Assets/Definitions/Tasks/test.json");
        }
    }
}