using System;

namespace FSG.Commands
{
	public struct StartGame : ICommand
	{
		public string Action { get => "START_GAME"; }
	}
}

