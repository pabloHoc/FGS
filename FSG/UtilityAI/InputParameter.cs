using System;
namespace FSG.UtilityAI
{
	public interface IInputParameter
	{
		public string Name { get; }

		public int Min { get; }

		public int Max { get; }
	}
}

