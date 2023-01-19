using System;

// TODO: check when we have to dispatch this command (building built, spell casted, etc)

namespace FSG.Commands
{
	public struct ComputeRegionsProduction : ICommand
	{
		public string Name { get => "ComputeRegionsProduction"; }
	}
}

