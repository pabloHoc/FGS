using System;

namespace FSG.UtilityAI
{
	public class Scorer
	{
		private readonly Curve _curve;

		private readonly InputParameter _inputParam;

		public Scorer(InputParameter inputParam, Curve curve)
		{
			_inputParam = inputParam;
			_curve = curve;
		}

		public double Score<Context, Target>(InputValue<Context, Target> inputValue)
			where Context : IState
		{
			var raw = inputValue.GetFrom(_inputParam);
			var normalized = Utils.Math.Normalize(raw, _inputParam.Min, _inputParam.Max);
			var finalScore = _curve.ComputeValue(normalized);
			return finalScore;
		}
	}
}

