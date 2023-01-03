using System;

namespace FSG.Commands
{
	public struct GenerateResources : ICommand
	{
		public string Action { get => "GENERATE_RESOURCES"; }
	}
}

