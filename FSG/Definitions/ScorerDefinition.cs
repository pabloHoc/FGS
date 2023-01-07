using System;
using FSG.Extensions;
using FSG.UtilityAI;

namespace FSG.Definitions
{
	public class ScorerDefinition : IDefinition
	{
		public DefinitionType Type => DefinitionType.Scorer;

        public string Name { get; init; }

		public Curve Curve { get; init; }

		public InputParameter Input { get; init; }

		//public ScorerDefinition()
		//{
		//	var curveStrParts = Curve.Split(" ");
		//	var curveType = Enum.Parse<CurveType>(curveStrParts[0].CapitalizeFirstLetter());
		//	var slope = Int32.Parse(curveStrParts[1]);
		//	var exponent = Int32.Parse(curveStrParts[2]);
		//	var xShift = Int32.Parse(curveStrParts[3]);
		//	var yShift = Int32.Parse(curveStrParts[4]);

		//	Scorer = new Scorer(Input, new Curve(
		//		curveType,
		//		slope,
		//		exponent,
		//		xShift,
		//		yShift
		//	));
		//}
    }
}

