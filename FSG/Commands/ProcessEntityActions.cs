using System;
using FSG.Entities;

namespace FSG.Commands
{
	public class ProcessEntityActions<T>: ICommand where T : IEntity<T>, IActorEntity
	{
		public string Name { get => "ProcessEntityActions"; }
	}
}

