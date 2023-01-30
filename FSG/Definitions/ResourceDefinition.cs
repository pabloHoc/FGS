using FSG.Commands;
using FSG.Scopes;

namespace FSG.Definitions
{
    public class ResourceDefinition : IDefinition
    {
        public DefinitionType DefinitionType => DefinitionType.Resource;

        public string Name { get; init; }

        public Scope Scope { get; init; }

        public Actions Actions { get; init; }
    }
}