using System;
namespace FSG.Commands
{
	public struct CreateEmpire : ICommand
	{
		public string Action { get => "CREATE_EMPIRE"; }

		public string Name { get; init; }
	}
}

