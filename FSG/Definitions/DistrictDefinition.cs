using System;
namespace FSG.Definitions
{
	public class DistrictDefinition : IDefinition
	{
        public DefinitionType Type => DefinitionType.District;

        public string Name { get; init; }

		public DistrictDefinition()
		{
		}
    }
}

