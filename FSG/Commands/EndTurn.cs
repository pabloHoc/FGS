using System;

namespace FSG.Commands
{
	public struct EndTurn : ICommand
	{
		public string Action { get => "END_TURN"; }
	}
}

