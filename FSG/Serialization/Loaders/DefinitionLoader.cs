using System.Collections.Generic;
using System.IO;
using FSG.Core;
using FSG.Definitions;

namespace FSG.Serialization
{
    public class DefinitionLoader
    {
        private readonly DefinitionRepository _repository;

        private const string DEFINITIONS_PATH = "../../../../FSG/Assets/Definitions/";

        public DefinitionLoader(DefinitionRepository repository)
        {
            this._repository = repository;
        }

        private void LoadDefinition<T>(string path) where T : IDefinition
        {
            var json = File.ReadAllText(DEFINITIONS_PATH + path);
            var definitions = Deserializer.Deserialize<List<T>>(json);
            definitions.ForEach(this._repository.Add);
        }

        public void LoadDefinitions()
        {
            this.LoadDefinition<BuildingDefinition>("Buildings/LandBuildings.json");
            this.LoadDefinition<BuildingDefinition>("Buildings/CapitalBuildings.json");
            this.LoadDefinition<BuildingDefinition>("Buildings/Districts.json");
            this.LoadDefinition<EconomicCategoryDefinition>("EconomicCategories/EconomicCategories.json");
            this.LoadDefinition<LandDefinition>("Lands/Lands.json");
            this.LoadDefinition<SetupConfigDefinition>("Configs/SetupConfigs.json");
            this.LoadDefinition<ScorerDefinition>("Scorers/Scorers.json");
            this.LoadDefinition<SocialStructureDefinition>("SocialStructures/SocialStructures.json");
            this.LoadDefinition<SpellDefinition>("Spells/Spells.json");
            this.LoadDefinition<ResourceDefinition>("Resources/Resources.json");
            this.LoadDefinition<TaskDefinition>("Tasks/test.json");
        }
    }
}