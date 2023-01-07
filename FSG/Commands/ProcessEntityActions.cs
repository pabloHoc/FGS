using System;
using FSG.Entities;

namespace FSG.Commands
{
	public class ProcessEntityActions<T>: ICommand where T : IEntity<T>, IActor
	{
		public string Name { get => "ProcessEntityActions"; }
	}
}

