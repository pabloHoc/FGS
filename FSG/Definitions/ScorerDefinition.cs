using System;
using FSG.Extensions;
using FSG.UtilityAI;

namespace FSG.Definitions
{
	public class ScorerDefinition : IDefinition
	{
		public DefinitionType DefinitionType => DefinitionType.Scorer;

        public string Name { get; init; }

		public Curve Curve { get; init; }

		public InputParameter Input { get; init; }

		public Scorer Scorer {
			get
			{
				if (_scorer == null)
				{
					_scorer = new Scorer(Input, Curve);
				}

				return _scorer;
			}
		}

		private Scorer _scorer;
    }
}

