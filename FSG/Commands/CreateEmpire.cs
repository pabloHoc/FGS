using System;
namespace FSG.Commands
{
	public struct CreateEmpire : ICommand
	{
		public string Name { get => "CreateEmpire"; }

		public string EmpireName { get; init; }
	}
}

