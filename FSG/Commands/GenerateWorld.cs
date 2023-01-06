using System;

namespace FSG.Commands
{
	public struct WorldGenerationOptions
	{
		public int Players { get; init; }
		public int LandsPerRegion { get; init; }
		public int Columns { get; init; }
		public int Rows { get; init; }
	}

	public struct GenerateWorld : ICommand
	{
		public string Name { get => "GenerateWorld"; }

		public WorldGenerationOptions Options { get; init; }
	}
}

