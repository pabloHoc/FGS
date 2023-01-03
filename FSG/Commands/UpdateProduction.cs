using System;

// TODO: check when we have to dispatch this command (building built, spell casted, etc)

namespace FSG.Commands
{
	public struct UpdateProduction : ICommand
	{
		public string Action { get => "UPDATE_PRODUCTION"; }
	}
}

